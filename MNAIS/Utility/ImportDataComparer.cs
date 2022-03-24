using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNAIS.Utility
{
    class ImportDataComparer:IEqualityComparer<ImportedData>
    {
        public bool Equals(ImportedData x, ImportedData y)
        {
            if (x.Block == y.Block)
            {
                if (x.BlockName == y.BlockName)
                {
                    if (x.AreaSown == y.AreaSown)
                    {
                        foreach (KeyValuePair<YearClass, YearClass> entry in Utility.Zip(x.YearData, y.YearData))
                        {
                            if (entry.Key.Data != entry.Value.Data)
                                return false;                            
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            return true;
        }

        public int GetHashCode(ImportedData obj)
        {
            return obj.Block.GetHashCode();
        }
    }
}
