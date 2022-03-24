using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Windows.Input;

namespace MNAIS
{
    [Serializable]    
    public class CreateContractViewModel : ViewModelBase
    {          
        ObservableCollection<string> m_district;

        [NonSerialized]
        RelayCommand saveCommand;

        [NonSerialized]
        internal MainWindowViewModel mainVM;

        string selectedDistrict;
        string contractName;
        int? contractYear;
        bool isSaveEnabled = true;

        DateTime policyStartDate = new DateTime(DateTime.Now.Year, 10, 1);
        DateTime policyEndDate = new DateTime(DateTime.Now.Year + 1, 10, 1);
        State selectedState;
        Season selectedSeason;
        Crop selectedCrop;
        DataSource selectedDataSource;
        IndemnityLevel selectedIndemnityLevel = IndemnityLevel.Ninety;
                
        #region Public Properties & Methods

        public bool IsSaveEnabled
        {
            get { return isSaveEnabled; }

            set
            {
                isSaveEnabled = value;
                OnPropertyChanged("IsSaveEnabled");
            }
        }

        public IndemnityLevel SelectedIndemnityLevel
        {
            get { return selectedIndemnityLevel; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedIndemnityLevel)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedIndemnityLevel = value;
                OnPropertyChanged("SelectedIndemnityLevel");
            }
        }

        public Season SelectedSeason
        {
            get { return selectedSeason; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedSeason)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedSeason = value;

                if (value == Season.Rabi)
                    policyStartDate = new DateTime(policyStartDate.Year, 10, policyStartDate.Day);
                else
                    policyStartDate = new DateTime(policyStartDate.Year, 4, policyStartDate.Day);

                OnPropertyChanged("SelectedSeason");
                OnPropertyChanged("PolicyStartDate");
            }
        }

        public Crop SelectedCrop
        {
            get { return selectedCrop; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedCrop)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedCrop = value;
                OnPropertyChanged("SelectedCrop");
            }
        }

        public DataSource SelectedDataSource
        {
            get { return selectedDataSource; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedDataSource)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedDataSource = value;
                OnPropertyChanged("SelectedDataSource");
            }
        }

        public string ContractName
        {
            get { return contractName; }

            set
            {
                if (mainVM != null)
                {
                    if (value != contractName)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                contractName = value;

                if (value.Length >= 50)
                    throw new ApplicationException("Length cannot be greater than 50");

                OnPropertyChanged("ContractName");
            }
        }

        public int? ContractYear
        {
            get { return contractYear; }

            set
            {
                if (mainVM != null)
                {
                    if (value != contractYear)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                contractYear = value;
                
                if (value != null)
                {
                    if (Utility.Utility.GetIntegerDigitCount(value) > 4)
                        throw new ApplicationException("Length cannot be greater than 4");

                    if (Utility.Utility.GetIntegerDigitCount(value) < 4)
                        throw new ApplicationException("Length cannot be less than 4");

                    if (Utility.Utility.GetIntegerDigitCount(value) == 4)
                    {
                        if (value < 2010)
                            throw new ApplicationException("Year cannot be less than 2010");

                        if (value > 2030)
                            throw new ApplicationException("Year cannot be greater than 2030");

                        if (selectedSeason == Season.Rabi)
                        {
                            policyStartDate = new DateTime(Convert.ToInt32(value), 10, 1);
                            policyEndDate = new DateTime(Convert.ToInt32(value+1), 1, 1);
                        }
                        else
                        {
                            policyStartDate = new DateTime(Convert.ToInt32(value), 4, 1);
                            policyEndDate = new DateTime(Convert.ToInt32(value + 1), 1, 1);
                        }
                    }                    
                }
                OnPropertyChanged("ContractYear"); 
                OnPropertyChanged("PolicyStartDate");
                OnPropertyChanged("PolicyEndDate");                               
            }
        }

        public DateTime PolicyStartDate
        {
            get 
            { 
                return policyStartDate; 
            }
            
            set
            {
                if (mainVM != null)
                {
                    if (value != policyStartDate)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                policyStartDate = value;
                OnPropertyChanged("PolicyStartDate");
            }
        }

        public DateTime PolicyEndDate
        {
            get 
            {                
               return policyEndDate; 
            }

            set
            {
                if (mainVM != null)
                {
                    if (value != policyEndDate)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                policyEndDate = value;

                if (value < PolicyStartDate)
                    throw new ApplicationException("Policy End Date cannot be less than Policy Start Date");

                TimeSpan difference = value.Subtract(PolicyStartDate);

                if(difference.Days >= 300)
                    throw new ApplicationException("Policy cannot be for more than 300 days");

                OnPropertyChanged("PolicyEndDate");
            }
        }

        public ObservableCollection<string> District
        {
            get
            {

                return m_district;
            }

            set
            {
                m_district = value;
                OnPropertyChanged("District");
            }
        }

        public string SelectedDistrict
        {
            get
            {
                if (string.IsNullOrEmpty(selectedDistrict))
                {
                    PopulateDistrict(SelectedState);
                }
                return selectedDistrict;
            }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedDistrict)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedDistrict = value;
                OnPropertyChanged("SelectedDistrict");
            }
        }

        public State SelectedState
        {
            get { return selectedState; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedState)
                    {
                        mainVM.ClearResults();
                        isSaveEnabled = true;
                        OnPropertyChanged("IsSaveEnabled");
                    }
                }

                selectedState = value;
                PopulateDistrict(value);
                OnPropertyChanged("SelectedState");
                OnPropertyChanged("District");
            }
        }

        public CreateContractViewModel(MainWindowViewModel mainWindowVM) 
        {
            mainVM = mainWindowVM;
        }

        public CreateContractViewModel() { }
                
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new RelayCommand(param => this.SaveContract());
                return saveCommand;
            }
        }
             
        #endregion

        #region Private Methods

        void PopulateDistrict(State selectedValue)
        {
            EnumerationExtension enumExtn;
            m_district = new ObservableCollection<string>();

            switch (selectedValue)
            {
                case State.AndamanandNicobarIslands:
                    enumExtn = new EnumerationExtension(typeof(AndamanandNicobarIslands));
                    foreach (AndamanandNicobarIslands district in enumExtn.ToList(typeof(AndamanandNicobarIslands)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.AndhraPradesh:
                    enumExtn = new EnumerationExtension(typeof(AndhraPradesh));
                    foreach (AndhraPradesh district in enumExtn.ToList(typeof(AndhraPradesh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.ArunachalPradesh:
                    enumExtn = new EnumerationExtension(typeof(ArunachalPradesh));
                    foreach (ArunachalPradesh district in enumExtn.ToList(typeof(ArunachalPradesh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Assam:
                    enumExtn = new EnumerationExtension(typeof(Assam));
                    foreach (Assam district in enumExtn.ToList(typeof(Assam)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Bihar:
                    enumExtn = new EnumerationExtension(typeof(Bihar));
                    foreach (Bihar district in enumExtn.ToList(typeof(Bihar)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Chandigarh:
                    enumExtn = new EnumerationExtension(typeof(Chandigarh));
                    foreach (Chandigarh district in enumExtn.ToList(typeof(Chandigarh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Chhattisgarh:
                    enumExtn = new EnumerationExtension(typeof(Chattisgarh));
                    foreach (Chattisgarh district in enumExtn.ToList(typeof(Chattisgarh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.DadraandNagarHaveli:
                    enumExtn = new EnumerationExtension(typeof(DadraandNagarHaveli));
                    foreach (DadraandNagarHaveli district in enumExtn.ToList(typeof(DadraandNagarHaveli)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.DamanandDiu:
                    enumExtn = new EnumerationExtension(typeof(DamanandDiu));
                    foreach (DamanandDiu district in enumExtn.ToList(typeof(DamanandDiu)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Delhi:
                    enumExtn = new EnumerationExtension(typeof(Delhi));
                    foreach (Delhi district in enumExtn.ToList(typeof(Delhi)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Goa:
                    enumExtn = new EnumerationExtension(typeof(Goa));
                    foreach (Goa district in enumExtn.ToList(typeof(Goa)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Gujarat:
                    enumExtn = new EnumerationExtension(typeof(Gujrat));
                    foreach (Gujrat district in enumExtn.ToList(typeof(Gujrat)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Haryana:
                    enumExtn = new EnumerationExtension(typeof(Haryana));
                    foreach (Haryana district in enumExtn.ToList(typeof(Haryana)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.HimachalPradesh:
                    enumExtn = new EnumerationExtension(typeof(HimachalPradesh));
                    foreach (HimachalPradesh district in enumExtn.ToList(typeof(HimachalPradesh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.JammuandKashmir:
                    enumExtn = new EnumerationExtension(typeof(JammuandKashmir));
                    foreach (JammuandKashmir district in enumExtn.ToList(typeof(JammuandKashmir)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Jharkhand:
                    enumExtn = new EnumerationExtension(typeof(Jharkhand));
                    foreach (Jharkhand district in enumExtn.ToList(typeof(Jharkhand)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Karnataka:
                    enumExtn = new EnumerationExtension(typeof(Karnataka));
                    foreach (Karnataka district in enumExtn.ToList(typeof(Karnataka)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Kerala:
                    enumExtn = new EnumerationExtension(typeof(Kerala));
                    foreach (Kerala district in enumExtn.ToList(typeof(Kerala)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Lakshadweep:
                    enumExtn = new EnumerationExtension(typeof(Lakshadweep));
                    foreach (Lakshadweep district in enumExtn.ToList(typeof(Lakshadweep)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.MadhyaPradesh:
                    enumExtn = new EnumerationExtension(typeof(MadhyaPradesh));
                    foreach (MadhyaPradesh district in enumExtn.ToList(typeof(MadhyaPradesh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Maharashtra:
                    enumExtn = new EnumerationExtension(typeof(Maharashtra));
                    foreach (Maharashtra district in enumExtn.ToList(typeof(Maharashtra)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Manipur:
                    enumExtn = new EnumerationExtension(typeof(Manipur));
                    foreach (Manipur district in enumExtn.ToList(typeof(Manipur)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Meghalaya:
                    enumExtn = new EnumerationExtension(typeof(Meghalaya));
                    foreach (Meghalaya district in enumExtn.ToList(typeof(Meghalaya)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Mizoram:
                    enumExtn = new EnumerationExtension(typeof(Mizoram));
                    foreach (Mizoram district in enumExtn.ToList(typeof(Mizoram)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Nagaland:
                    enumExtn = new EnumerationExtension(typeof(Nagaland));
                    foreach (Nagaland district in enumExtn.ToList(typeof(Nagaland)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Odisha:
                    enumExtn = new EnumerationExtension(typeof(Orissa));
                    foreach (Orissa district in enumExtn.ToList(typeof(Orissa)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Puducherry:
                    enumExtn = new EnumerationExtension(typeof(Puducherry));
                    foreach (Puducherry district in enumExtn.ToList(typeof(Puducherry)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Punjab:
                    enumExtn = new EnumerationExtension(typeof(Punjab));
                    foreach (Punjab district in enumExtn.ToList(typeof(Punjab)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Rajasthan:
                    enumExtn = new EnumerationExtension(typeof(Rajasthan));
                    foreach (Rajasthan district in enumExtn.ToList(typeof(Rajasthan)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Sikkim:
                    enumExtn = new EnumerationExtension(typeof(Sikkim));
                    foreach (Sikkim district in enumExtn.ToList(typeof(Sikkim)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.TamilNadu:
                    enumExtn = new EnumerationExtension(typeof(TamilNadu));
                    foreach (TamilNadu district in enumExtn.ToList(typeof(TamilNadu)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Tripura:
                    enumExtn = new EnumerationExtension(typeof(Tripura));
                    foreach (Tripura district in enumExtn.ToList(typeof(Tripura)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.Uttarakhand:
                    enumExtn = new EnumerationExtension(typeof(Uttarakhand));
                    foreach (Uttarakhand district in enumExtn.ToList(typeof(Uttarakhand)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.UttarPradesh:
                    enumExtn = new EnumerationExtension(typeof(UttarPradesh));
                    foreach (UttarPradesh district in enumExtn.ToList(typeof(UttarPradesh)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                case State.WestBengal:
                    enumExtn = new EnumerationExtension(typeof(WestBengal));
                    foreach (WestBengal district in enumExtn.ToList(typeof(WestBengal)))
                    {
                        m_district.Add(enumExtn.GetDescription(district));
                    }
                    break;

                default:
                    break;
            }

            if (m_district.Count > 0)
            {
                selectedDistrict = m_district.First();
            }
            OnPropertyChanged("SelectedDistrict");
        }

        void SaveContract()
        {
            if (Validate())
            {
                mainVM.ErrType = ErrorType.Debug;
                mainVM.Message = "Contract saved successfully.";
                mainVM.IsDataImportEnabled = true;

                mainVM.PresenterContent = mainVM.m_MainWindow.HomeScreen;
                mainVM.ProjectInfoPresenter = mainVM.m_MainWindow.ContractInfoScreen;

                mainVM.IsCreateEnabled = false;
                mainVM.IsDeleteEnabled = true;

                isSaveEnabled = false;
                OnPropertyChanged("IsSaveEnabled");
            }
            else
            {
                mainVM.ErrType = ErrorType.Warning;

                if (mainVM.IsDeleteEnabled)
                    mainVM.IsDeleteEnabled = false;

                if (mainVM.IsCreateEnabled)
                    mainVM.IsCreateEnabled = true;

                if (mainVM.IsDataImportEnabled)
                    mainVM.IsDataImportEnabled = false;

                mainVM.ErrType = ErrorType.Error;
                mainVM.Message = "Contract Name and Year should not be left blank.";
            }
        }

        bool Validate()
        {
            if (!string.IsNullOrEmpty(this.ContractName) && this.ContractYear != null)
                return true;
            else
                return false;
        }

        #endregion
    }
}
