using System.Collections.Generic;
using Newtonsoft.Json;

namespace APIMock.Models.AuxiliaryModels
{    
    public class DataTableResult<T>
    {           
        public int TotalRegistros { get; set; }                
        public List<T>? PaginaRegistros { get; set; }       
    }
    
    public class DataTableParameters
    {        
        public FiltroPorId[]? Filtros { get; set; }
        public Ordenacao? Ordenacao { get; set; }      
        public int Pagina { get; set; }
        public int ItemsPorPagina { get; set; }
    }
    
    public class FiltroPorId
    {                
        public string Coluna { get; set; }        
        public string[] Ids { get; set; }
    }
    public class FiltroPorTexto
    {                
        public string Coluna { get; set; }        
        public string Texto { get; set; }
    }

    public class Ordenacao
    {
        public string Coluna { get; set; }
        public Direcao Dir { get; set; }
    }
        
    public enum Direcao
    {
        Asc,
        Desc
    }   
}
