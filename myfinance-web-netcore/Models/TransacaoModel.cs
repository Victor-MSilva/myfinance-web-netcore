namespace myfinance_web_netcore.Models
{
    public class TransacaoModel
    {   
        public int? Id { get; set; }
        public DateTime Data {get; set; }
        public decimal Valor {get; set; }
        public string? Historico { get; set; }
        public string? Tipo { get; set; }
        public int IdPlanoConta {get; set; }
        
    }

    public class TransacaoModelMin
    { 
        public decimal Valor {get; set; }
        public string? Tipo { get; set; }
        
    }
}