namespace myfinance_web_netcore.Models
{
    public class TransacaoReportModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<TransacaoModelMin> Transacao { get; set; }

        public TransacaoReportModel()
        {
            Transacao = new List<TransacaoModelMin>();
        }        
    }
}