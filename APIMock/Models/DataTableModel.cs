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
        public FiltroPorId[]? Filters { get; set; }
        public Ordenacao? Order { get; set; }      
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
    
    public class FiltroPorId
    {                
        public string Column { get; set; }        
        public string[] Ids { get; set; }
    }
    public class FiltroPorTexto
    {                
        public string Column { get; set; }        
        public string Text { get; set; }
    }

    public class Ordenacao
    {
        public string Column { get; set; }
        public Direcao Dir { get; set; }
    }
        
    public enum Direcao
    {
        Asc,
        Desc
    }   
}
