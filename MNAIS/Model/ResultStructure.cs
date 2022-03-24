using System.Collections.Generic;

namespace MNAIS
{
    public class ResultStructure
    {
        public string Catagory { get; set; }
        public string Description { get; set; }        
        public List<StateClass> Data { get; set; }
    }

    public class StateClass
    {
        public string Header { get; set; }
        public string Value { get; set; }
    }
}
