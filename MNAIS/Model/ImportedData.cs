using System;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <Summary>Structure for Yield Data Import</Summary>
/// <Author>Mani Sharma</Author>
/// <Date Created></Date Created>
/// <Date Modified></Date Modified>
/// <Modified By></Modified By>

namespace MNAIS
{
    [Serializable]
    public class ImportedData : IDataErrorInfo, INotifyPropertyChanged,ICloneable
    {
        static readonly Regex MyNameEx = new Regex(@"[<>?*!@$#\\""/|]");
        string block;
        string blockName;
        string areaSown;
        ObservableCollection<YearClass> yearData;

        public string Block
        {
            get { return block; }

            set
            {
                block = value;
                NotifyErrorChanged();
            }
        }

        public string BlockName
        {
            get { return blockName; }

            set
            {
                blockName = value;
                NotifyErrorChanged();
            }
        }

        public string AreaSown
        {
            get { return areaSown; }

            set
            {
                areaSown = value;
                NotifyErrorChanged();
            }
        }

        public ObservableCollection<YearClass> YearData
        {
            get { return yearData; }

            set
            {
                yearData = value;
                NotifyErrorChanged();
            }
        }

        string TrimString(string input)
        {
            return input.Replace('"', ' ').Trim();
        }

        public ObservableCollection<ImportedData> AddImportedData(string filepath)
        {
            ObservableCollection<ImportedData> importedData = new ObservableCollection<ImportedData>();

            string[] Lines = File.ReadAllLines(filepath);
            string[] Fields;
            Fields = Lines[0].TrimEnd(',').Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            for (int i = 1; i < Lines.Count(); i++)
            {
                YearData = new ObservableCollection<YearClass>();
                
                for (int j = 3; j < Cols; j++)
                {
                    YearData.Add(new YearClass { Header = TrimString(Fields[j]), Data = 0 });
                }

                string[] temp = Lines[i].TrimEnd(',').Split(new char[] { ',' });                                
                int data = 3;
                int count = 0;
                foreach (YearClass entry in YearData)
                {
                    if (data >= temp.Count())
                    {
                        entry.Data = null;
                    }
                    else
                    {
                        if (temp[data] != string.Empty)
                        {
                            entry.Data = Convert.ToDouble(TrimString(temp[data]));
                            count++;
                        }
                        else
                        {
                            entry.Data = null;
                        }                        
                    }
                    data++;
                }

                if (count < 3)
                    throw new Exception("Minimum 3 years of data is required.");

                ImportedData newEntry = new ImportedData { 
                    Block = TrimString(temp[0]), 
                    BlockName = TrimString(temp[1]),
                    AreaSown = TrimString(temp[2]) };

                newEntry.YearData = YearData;

                importedData.Add(newEntry);
            }

            return importedData;
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void NotifyErrorChanged()
        {
            NotifyPropertyChanged("Error");
        }

        # endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                StringBuilder error = new StringBuilder();

                // iterate over all of the properties
                // of this object - aggregating any validation errors
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);

                foreach (PropertyDescriptor prop in props)
                {
                    String propertyError = this[prop.Name];
                    if (propertyError != string.Empty)
                    {
                        error.Append((error.Length != 0 ? ", " : "") + propertyError);
                    }
                }

                return error.Length == 0 ? null : error.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Block")
                {
                    if (String.IsNullOrEmpty(this.Block))
                        return "Block needs to be present";

                    if (MyNameEx.Match(this.Block.ToString()).Success)
                        return "Block may only contain numbers";
                }

                if (columnName == "BlockName")
                {
                    if (String.IsNullOrEmpty(this.BlockName))
                        return "BlockName needs to be present";

                    if (MyNameEx.Match(this.BlockName).Success)
                        return "BlockName may only contain characters";
                }

                if (columnName == "AreaSown")
                {
                    if (String.IsNullOrEmpty(this.AreaSown))
                        return "AreaSown needs to be present";

                    if (MyNameEx.Match(this.AreaSown.ToString()).Success)
                        return "Block may only contain numbers";
                }

                if (columnName == "YearData")
                {
                    foreach (YearClass entry in this.YearData)
                    {
                        if (entry.Data == null)
                            return "Data needs to be present";
                    }
                }

                return string.Empty;
            }
        }

        # endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <Summary>Supporting Structure for Yield Data Import</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created></Date Created>
    /// <Date Modified></Date Modified>
    /// <Modified By></Modified By>

    [Serializable]
    public class YearClass : IDataErrorInfo, INotifyPropertyChanged, ICloneable
    {
        static readonly Regex MyNameEx = new Regex(@"[<>?*!@$#\\""/|]");
        string header;
        double? data;
        
        public string Header
        {
            get { return header; }

            set
            {
                header = value;
                NotifyErrorChanged();
            }
        }

        public double? Data
        {
            get { return data; }

            set
            {
                data = value;
                NotifyErrorChanged();
            }
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void NotifyErrorChanged()
        {
            NotifyPropertyChanged("Error");
        }

        # endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                StringBuilder error = new StringBuilder();                
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);

                foreach (PropertyDescriptor prop in props)
                {
                    String propertyError = this[prop.Name];
                    if (propertyError != string.Empty)
                    {
                        error.Append((error.Length != 0 ? ", " : "") + propertyError);
                    }
                }

                return error.Length == 0 ? null : error.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Data")
                {
                    if (this.Data == null)
                        return "Data needs to be present";

                    if (MyNameEx.Match(this.Data.ToString()).Success)
                        return "Data may only contain numbers";
                }
                
                return string.Empty;
            }
        }

        # endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
