using System;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace MNAIS.Analytics
{
    /// <Summary>Main Class for Analytical Module</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>
    
    public static class Analytics
    {
        public static MainWindowViewModel mainVM { get; set; }

        /// <summary>
        /// Main analytics method to be called from UI.
        /// </summary>
        public static void Run()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                ObservableCollection<ModifiedUserData> userData = Utility.ConvertInputData(mainVM.ImportDataVM.import, mainVM.ModellingVM.MovingAverage);
                Core.ComputeParams(userData, null);
                ObservableCollection<ModifiedUserData> modelledData = Core.ComputeModelledYields(userData, 0, 0);
                Core.ComputeParams(userData, modelledData);

                #region Detrending

                ObservableCollection<ModifiedUserData> detrendedData =
                    Core.DetrendData(userData, Convert.ToInt32(mainVM.CreateContractVM.ContractYear), mainVM.ModellingVM.SelectedSignificanceForTrending);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(userData, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\UserData.csv");
                    Utility.WriteFile(detrendedData, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DetrendedData.csv");
                }

                #endregion

                # region Simulation Yield Generator

                ObservableCollection<ModifiedUserData> simulatedYieldData = Core.GenerateSimulatedYield(userData, detrendedData, mainVM.ModellingVM.CorrelationInput);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(simulatedYieldData, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SimulatedYieldData.csv");
                }

                # endregion

                # region Historical Yield Generator

                ObservableCollection<ModifiedUserData> completeModelledData =
                    Core.ComputeModelledYields(userData, mainVM.CreateContractVM.PolicyStartDate.Year, mainVM.CreateContractVM.PolicyEndDate.Year);

                ObservableCollection<ModifiedUserData> historicalYieldData =
                    Core.GenerateHistoricalYield(userData, completeModelledData, mainVM.ModellingVM.SelectedDataProcessed, mainVM.ModellingVM.SelectedDataGaps,
                    Convert.ToInt32(mainVM.ImportDataVM.StartYear));

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(historicalYieldData, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HistoricalYieldData.csv");
                }

                # endregion

                # region Historical Loss Analysis

                //compute threshold yields
                ObservableCollection<ModifiedUserData> thresholdYields = Core.HistoricalLossAnalysisThresholdYield(historicalYieldData, Convert.ToInt32(mainVM.CreateContractVM.ContractYear),
                    mainVM.ModellingVM.MovingAverage, mainVM.ModellingVM.SelectedCalamityYears, mainVM.CreateContractVM.SelectedIndemnityLevel);

                //compute loss ratios
                ObservableCollection<ModifiedUserData> historicalLossAnalysis = Core.HistoricalLossAnalysis(thresholdYields, historicalYieldData,
                    mainVM.CreateContractVM.PolicyStartDate.Year, Convert.ToInt32(mainVM.ImportDataVM.StartYear), mainVM.PricingVM);

                //compute district level loss ratios
                ObservableCollection<ModifiedUserData> historicalDistrictLossAnalysis = Core.HistoricalDistrictLossAnalysis(historicalLossAnalysis, userData);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(historicalLossAnalysis, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HistoricalLossAnalysis.csv");
                    Utility.WriteFile(historicalDistrictLossAnalysis, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HistoricalDistrictLossAnalysis.csv");
                }

                # endregion

                # region Simulated Loss Analysis

                //threshold yield currently calculated using historical data, could be changed if required.
                ObservableCollection<ThresholdYield> thyData = Core.SimulatedLossAnalysisThresholdYield(historicalYieldData, mainVM.ModellingVM.SelectedCalamityYears,
                    Convert.ToInt32(mainVM.ModellingVM.MovingAverage), mainVM.CreateContractVM.SelectedIndemnityLevel);

                //calculate simulation data
                ObservableCollection<ModifiedUserData> simulation = Core.Simulation(thyData, simulatedYieldData,mainVM.ModellingVM, mainVM.PricingVM);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFileForSimulation(simulation, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SimulationData.csv");
                }

                # endregion

                # region Correlation Matrix

                double?[,] correlationMatrix = new double?[,] { };

                if (mainVM.ModellingVM.SelectedSimulationMethod == SimulationMethod.YieldCorrelation)
                    correlationMatrix = Core.CorrelationMatrix(simulatedYieldData);
                else if (mainVM.ModellingVM.SelectedSimulationMethod == SimulationMethod.PayoutCorrelation)
                    correlationMatrix = Core.CorrelationMatrix(historicalLossAnalysis);
         
                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteCorrelationMatrixToFile(correlationMatrix, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CorrelationMatrix.csv");
                }

                # endregion

                # region Aggregation

                ModifiedUserData historicalAggregation = Core.HistoricalAggregation(historicalDistrictLossAnalysis);
                ModifiedUserData simulationAggregation = Core.SimulationAggregation(simulation, correlationMatrix);
                
                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteAggregationTofile(historicalAggregation, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HistoricalAggregation.csv");
                    Utility.WriteAggregationTofile(simulationAggregation, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SimulationAggregation.csv");
                }

                # endregion

                # region Simulation Pricing

                ObservableCollection<Result> SimulationPricing = Core.SimulatedPricing(mainVM.ModellingVM, mainVM.PricingVM, simulation,historicalLossAnalysis);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(SimulationPricing, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SimulatedPricingResults.csv");
                }

                # endregion

                # region Historical Pricing

                ObservableCollection<Result> HistoricalPricing = Core.HistoricalPricing(mainVM.ModellingVM, mainVM.PricingVM, historicalLossAnalysis);

                if (mainVM.AdminVM.GenerateLogFile == true)
                {
                    Utility.WriteFile(HistoricalPricing, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HistoricalPricingResults.csv");
                }

                # endregion

                # region Aggregate Results

                Result aggregateSimulationPricing = new Result();
                Result aggregateHistoricalPricing = new Result();
                                
                aggregateSimulationPricing = Core.AggregateSimulatedPricing(mainVM.ModellingVM, mainVM.PricingVM, historicalAggregation.Count,
                        simulationAggregation.Mean, simulationAggregation.StdDev);
                
                aggregateHistoricalPricing = Core.AggregateHistoricalPricing(mainVM.ModellingVM, mainVM.PricingVM, historicalAggregation.Count,
                        historicalAggregation.Mean, historicalAggregation.StdDev, historicalDistrictLossAnalysis);

                mainVM.ResultsSummaryVM.SimulationResult = SimulationPricing;
                mainVM.ResultsSummaryVM.HistoricalResult = HistoricalPricing;

                mainVM.ResultsSummaryVM.SimulationResult.Insert(0, aggregateSimulationPricing);
                mainVM.ResultsSummaryVM.HistoricalResult.Insert(0, aggregateHistoricalPricing);

                mainVM.ResultsSummaryVM.mainVM = mainVM;

                # endregion
                                
                stopWatch.Stop();
                mainVM.ErrType = ErrorType.Debug;
                mainVM.Message = "Analysis completed in : " + stopWatch.Elapsed.TotalSeconds + " seconds";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }        
    }
}
