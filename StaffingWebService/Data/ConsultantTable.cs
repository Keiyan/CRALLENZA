
//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Cellenza.Service.Data
{

using System;
    using System.Collections.Generic;
    
public partial class ConsultantTable
{

    public ConsultantTable()
    {

        this.Actif = true;

        this.Admin = false;

        this.CompteRenduActiviteTable = new HashSet<CompteRenduActiviteTable>();

        this.VacanceTable = new HashSet<VacanceTable>();

    }


    public int ID { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public Nullable<System.DateTime> DateNaissance { get; set; }

    public string NoSecu { get; set; }

    public string Email { get; set; }

    public Nullable<System.DateTime> DateArrivee { get; set; }

    public Nullable<System.DateTime> DateDepart { get; set; }

    public string Image { get; set; }

    public bool Actif { get; set; }

    public bool Admin { get; set; }

    public string Poste { get; set; }

    public string AdresseRue { get; set; }

    public string AdresseComplement { get; set; }

    public string AdresseCodePostal { get; set; }

    public string AdresseVille { get; set; }

    public string LiveEmail { get; set; }



    public virtual ICollection<CompteRenduActiviteTable> CompteRenduActiviteTable { get; set; }

    public virtual ICollection<VacanceTable> VacanceTable { get; set; }

}

}
