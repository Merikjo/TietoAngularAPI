using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TietoAngularAPI.ViewModels
{
    public class SimplyTunnitData
    {
        public int Tunti_id { get; set; }
        public int Henkilo_id { get; set; }
        public int Projekti_id { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Osoite { get; set; }
        public string Postinumero { get; set; }
        public Nullable<int> Esimies { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Pvm { get; set; }
        //public string Pvm { get; set; }

        public DateTime? Avattu { get; set; }
        public DateTime? Suljettu { get; set; }

        public int? ProjektiTunnit { get; set; }
        public string ProjektiNimi { get; set; }

        public string Status { get; set; }

        [Display(Name = "Henkilön nimi")]
        public string KokonimiH2 { get; set; }

        [Display(Name = "Henkilön nimi")]
        public string KokonimiH
        {
            get { return Etunimi + " " + Sukunimi; }
            set { KokonimiH2 = value; }
        }
    }
}