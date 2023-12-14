using System.Collections.Generic;
using Newtonsoft.Json;

namespace APIMock.Models.AuxiliaryModels
{    
    public class DataTableResult<T>
    {           
        public int Total { get; set; }                
        public int Total_Pages { get; set; }                
        public int Page { get; set; }                
        public int Per_Page { get; set; }                
        public List<T>? Data { get; set; }       
    }
    
    public class DataTableParameters
    {        
        public Filtro Filters { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
    
    public class Filtro
    {                
        public string[] Status { get; set; } 
        public string[] Products { get; set; }
        public string[] Comissioneds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Keyword {  get; set; }
    }   
      
}
