namespace Service_Incidents.Models
{
    public class Incident
    {
        public int INCD_ID { get; set; }
        public int INCD_ENTT_SG_ID { get; set; }
        public int INCD_TYPE_ID { get; set; }
        public int INCD_STAT_ID { get; set; }
        public int INCD_PRIO_ID { get; set; }
        public int INCD_UTIL_ID { get; set; }
        public int INCD_NUM_TICK { get; set; }
        public string? INCD_DESC { get; set; }
        public string? agn_code { get; set; }
        public DateTime incd_date_creation { get; set; }
        public DateTime? incd_date_resolution { get; set; }
        public DateTime? incd_date_cloture { get; set; }
        public string? incd_audit { get; set; }
        public int? pres_id { get; set; }
        public int? niveau_escalade { get; set; }
        public string? Phone { get; set; }
        public bool IsSendSms1 { get; set; }
        public bool IsSendSms2 { get; set; }
        public bool IsSendSms3 { get; set; }
        public bool IsSendSms4 { get; set; }
        public int Motif_id { get; set; }

     
    }

    public class Types
    {
        public int INCD_TYPE_ID { get; set; }
        public string? TYPE_DESC { get; set; }
    }

    public class Statut
    {
        public int INCD_STAT_ID { get; set; }
        public string? STAT_DESC { get; set; }
    }

    public class Priorite
    {
        public int INCD_PRIO_ID { get; set; }
        public string? PRIO_DESC { get; set; }
    }

    public class Ticket
    { public int INCD_NUM_TICK { get; set; }
    
    public string? TICK_DESC { get; set; }

    }
 

}
