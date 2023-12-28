using System.Collections.Generic;
using Newtonsoft.Json;

namespace APIMock.Models.AuxiliaryModels
{    
    public class DataTableResult<T>
    {           
        public int DataCount { get; set; }                
        public int PageCount { get; set; }                
        public int PageIndex { get; set; }                
        public int PageSize { get; set; }                
        public List<T>? Page { get; set; }       
    }
    
    public class DataTableParameters
    {        
        public Filtro Filters { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int DataCount { get; set; }

    }
    
    public class Filtro
    {                
        public string[] Status { get; set; } 
        public string[] Products { get; set; }
        public string[] Commissioneds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? SearchCriteria {  get; set; }
    }   
      
}
