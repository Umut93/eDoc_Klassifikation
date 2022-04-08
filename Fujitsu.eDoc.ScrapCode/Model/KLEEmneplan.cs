
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("KLE-Emneplan", Namespace = "", IsNullable = false)]
public partial class KLEEmneplan
{

    private System.DateTime udgivelsesDatoField;

    private KLEEmneplanHovedgruppe[] hovedgruppeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime UdgivelsesDato
    {
        get
        {
            return this.udgivelsesDatoField;
        }
        set
        {
            this.udgivelsesDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Hovedgruppe")]
    public KLEEmneplanHovedgruppe[] Hovedgruppe
    {
        get
        {
            return this.hovedgruppeField;
        }
        set
        {
            this.hovedgruppeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppe
{

    private string hovedgruppeNrField;

    private string hovedgruppeTitelField;

    private KLEEmneplanHovedgruppeHovedgruppeVejledning hovedgruppeVejledningField;

    private KLEEmneplanHovedgruppeHovedgruppeAdministrativInfo hovedgruppeAdministrativInfoField;

    private string[] hovedgruppeFilterField;

    private KLEEmneplanHovedgruppeGruppe[] gruppeField;

    /// <remarks/>
    public string HovedgruppeNr
    {
        get
        {
            return this.hovedgruppeNrField;
        }
        set
        {
            this.hovedgruppeNrField = value;
        }
    }

    /// <remarks/>
    public string HovedgruppeTitel
    {
        get
        {
            return this.hovedgruppeTitelField;
        }
        set
        {
            this.hovedgruppeTitelField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeHovedgruppeVejledning HovedgruppeVejledning
    {
        get
        {
            return this.hovedgruppeVejledningField;
        }
        set
        {
            this.hovedgruppeVejledningField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeHovedgruppeAdministrativInfo HovedgruppeAdministrativInfo
    {
        get
        {
            return this.hovedgruppeAdministrativInfoField;
        }
        set
        {
            this.hovedgruppeAdministrativInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("FilterTitel", IsNullable = false)]
    public string[] HovedgruppeFilter
    {
        get
        {
            return this.hovedgruppeFilterField;
        }
        set
        {
            this.hovedgruppeFilterField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Gruppe")]
    public KLEEmneplanHovedgruppeGruppe[] Gruppe
    {
        get
        {
            return this.gruppeField;
        }
        set
        {
            this.gruppeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeVejledning
{

    private KLEEmneplanHovedgruppeHovedgruppeVejledningP[] vejledningTekstField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("p", IsNullable = false)]
    public KLEEmneplanHovedgruppeHovedgruppeVejledningP[] VejledningTekst
    {
        get
        {
            return this.vejledningTekstField;
        }
        set
        {
            this.vejledningTekstField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeVejledningP
{

    private string[] ulField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("li", IsNullable = false)]
    public string[] ul
    {
        get
        {
            return this.ulField;
        }
        set
        {
            this.ulField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeAdministrativInfo
{

    private object[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Historisk", typeof(KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistorisk))]
    [System.Xml.Serialization.XmlElementAttribute("OprettetDato", typeof(System.DateTime), DataType = "date")]
    [System.Xml.Serialization.XmlElementAttribute("RettetDato", typeof(System.DateTime), DataType = "date")]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistorisk
{

    private KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttet flyttetField;

    private System.DateTime udgaaetDatoField;

    private bool udgaaetDatoFieldSpecified;

    /// <remarks/>
    public KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttet Flyttet
    {
        get
        {
            return this.flyttetField;
        }
        set
        {
            this.flyttetField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime UdgaaetDato
    {
        get
        {
            return this.udgaaetDatoField;
        }
        set
        {
            this.udgaaetDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UdgaaetDatoSpecified
    {
        get
        {
            return this.udgaaetDatoFieldSpecified;
        }
        set
        {
            this.udgaaetDatoFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttet
{

    private System.DateTime flyttetDatoField;

    private KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttetAfloestAf afloestAfField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime FlyttetDato
    {
        get
        {
            return this.flyttetDatoField;
        }
        set
        {
            this.flyttetDatoField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttetAfloestAf AfloestAf
    {
        get
        {
            return this.afloestAfField;
        }
        set
        {
            this.afloestAfField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeHovedgruppeAdministrativInfoHistoriskFlyttetAfloestAf
{

    private string emneNrField;

    /// <remarks/>
    public string EmneNr
    {
        get
        {
            return this.emneNrField;
        }
        set
        {
            this.emneNrField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType
{

    /// <remarks/>
    Historisk,

    /// <remarks/>
    OprettetDato,

    /// <remarks/>
    RettetDato,
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppe
{

    private decimal gruppeNrField;

    private string gruppeTitelField;

    private KLEEmneplanHovedgruppeGruppeGruppeVejledning gruppeVejledningField;

    private KLEEmneplanHovedgruppeGruppeGruppeRetskildeReference[] gruppeRetskildeReferenceField;

    private KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfo gruppeAdministrativInfoField;

    private string[] gruppeFilterField;

    private KLEEmneplanHovedgruppeGruppeEmne[] emneField;

    /// <remarks/>
    public decimal GruppeNr
    {
        get
        {
            return this.gruppeNrField;
        }
        set
        {
            this.gruppeNrField = value;
        }
    }

    /// <remarks/>
    public string GruppeTitel
    {
        get
        {
            return this.gruppeTitelField;
        }
        set
        {
            this.gruppeTitelField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeGruppeVejledning GruppeVejledning
    {
        get
        {
            return this.gruppeVejledningField;
        }
        set
        {
            this.gruppeVejledningField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GruppeRetskildeReference")]
    public KLEEmneplanHovedgruppeGruppeGruppeRetskildeReference[] GruppeRetskildeReference
    {
        get
        {
            return this.gruppeRetskildeReferenceField;
        }
        set
        {
            this.gruppeRetskildeReferenceField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfo GruppeAdministrativInfo
    {
        get
        {
            return this.gruppeAdministrativInfoField;
        }
        set
        {
            this.gruppeAdministrativInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("FilterTitel", IsNullable = false)]
    public string[] GruppeFilter
    {
        get
        {
            return this.gruppeFilterField;
        }
        set
        {
            this.gruppeFilterField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Emne")]
    public KLEEmneplanHovedgruppeGruppeEmne[] Emne
    {
        get
        {
            return this.emneField;
        }
        set
        {
            this.emneField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeVejledning
{

    private KLEEmneplanHovedgruppeGruppeGruppeVejledningP[] vejledningTekstField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("p", IsNullable = false)]
    public KLEEmneplanHovedgruppeGruppeGruppeVejledningP[] VejledningTekst
    {
        get
        {
            return this.vejledningTekstField;
        }
        set
        {
            this.vejledningTekstField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeVejledningP
{

    private string[] ulField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("li", IsNullable = false)]
    public string[] ul
    {
        get
        {
            return this.ulField;
        }
        set
        {
            this.ulField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeRetskildeReference
{

    private string retskildeTitelField;

    private string paragrafEllerKapitelField;

    private string retsinfoAccessionsNrField;

    /// <remarks/>
    public string RetskildeTitel
    {
        get
        {
            return this.retskildeTitelField;
        }
        set
        {
            this.retskildeTitelField = value;
        }
    }

    /// <remarks/>
    public string ParagrafEllerKapitel
    {
        get
        {
            return this.paragrafEllerKapitelField;
        }
        set
        {
            this.paragrafEllerKapitelField = value;
        }
    }

    /// <remarks/>
    public string RetsinfoAccessionsNr
    {
        get
        {
            return this.retsinfoAccessionsNrField;
        }
        set
        {
            this.retsinfoAccessionsNrField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfo
{

    private System.DateTime oprettetDatoField;

    private System.DateTime rettetDatoField;

    private bool rettetDatoFieldSpecified;

    private KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistorisk historiskField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime OprettetDato
    {
        get
        {
            return this.oprettetDatoField;
        }
        set
        {
            this.oprettetDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime RettetDato
    {
        get
        {
            return this.rettetDatoField;
        }
        set
        {
            this.rettetDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool RettetDatoSpecified
    {
        get
        {
            return this.rettetDatoFieldSpecified;
        }
        set
        {
            this.rettetDatoFieldSpecified = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistorisk Historisk
    {
        get
        {
            return this.historiskField;
        }
        set
        {
            this.historiskField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistorisk
{

    private KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttet flyttetField;

    private System.DateTime udgaaetDatoField;

    private bool udgaaetDatoFieldSpecified;

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttet Flyttet
    {
        get
        {
            return this.flyttetField;
        }
        set
        {
            this.flyttetField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime UdgaaetDato
    {
        get
        {
            return this.udgaaetDatoField;
        }
        set
        {
            this.udgaaetDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UdgaaetDatoSpecified
    {
        get
        {
            return this.udgaaetDatoFieldSpecified;
        }
        set
        {
            this.udgaaetDatoFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttet
{

    private System.DateTime flyttetDatoField;

    private KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttetAfloestAf afloestAfField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime FlyttetDato
    {
        get
        {
            return this.flyttetDatoField;
        }
        set
        {
            this.flyttetDatoField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttetAfloestAf AfloestAf
    {
        get
        {
            return this.afloestAfField;
        }
        set
        {
            this.afloestAfField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeGruppeAdministrativInfoHistoriskFlyttetAfloestAf
{

    private decimal gruppeNrField;

    private bool gruppeNrFieldSpecified;

    private string emneNrField;

    /// <remarks/>
    public decimal GruppeNr
    {
        get
        {
            return this.gruppeNrField;
        }
        set
        {
            this.gruppeNrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool GruppeNrSpecified
    {
        get
        {
            return this.gruppeNrFieldSpecified;
        }
        set
        {
            this.gruppeNrFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string EmneNr
    {
        get
        {
            return this.emneNrField;
        }
        set
        {
            this.emneNrField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmne
{

    private string emneNrField;

    private string emneTitelField;

    private KLEEmneplanHovedgruppeGruppeEmneEmneVejledning emneVejledningField;

    private KLEEmneplanHovedgruppeGruppeEmneEmneRetskildeReference[] emneRetskildeReferenceField;

    private KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfo emneAdministrativInfoField;

    private string bevaringJaevnfoerArkivlovenField;

    private string sletningJaevnfoerPersondatalovenField;

    private string[] emneFilterField;

    /// <remarks/>
    public string EmneNr
    {
        get
        {
            return this.emneNrField;
        }
        set
        {
            this.emneNrField = value;
        }
    }

    /// <remarks/>
    public string EmneTitel
    {
        get
        {
            return this.emneTitelField;
        }
        set
        {
            this.emneTitelField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeEmneEmneVejledning EmneVejledning
    {
        get
        {
            return this.emneVejledningField;
        }
        set
        {
            this.emneVejledningField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("EmneRetskildeReference")]
    public KLEEmneplanHovedgruppeGruppeEmneEmneRetskildeReference[] EmneRetskildeReference
    {
        get
        {
            return this.emneRetskildeReferenceField;
        }
        set
        {
            this.emneRetskildeReferenceField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfo EmneAdministrativInfo
    {
        get
        {
            return this.emneAdministrativInfoField;
        }
        set
        {
            this.emneAdministrativInfoField = value;
        }
    }

    /// <remarks/>
    public string BevaringJaevnfoerArkivloven
    {
        get
        {
            return this.bevaringJaevnfoerArkivlovenField;
        }
        set
        {
            this.bevaringJaevnfoerArkivlovenField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
    public string SletningJaevnfoerPersondataloven
    {
        get
        {
            return this.sletningJaevnfoerPersondatalovenField;
        }
        set
        {
            this.sletningJaevnfoerPersondatalovenField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("FilterTitel", IsNullable = false)]
    public string[] EmneFilter
    {
        get
        {
            return this.emneFilterField;
        }
        set
        {
            this.emneFilterField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneVejledning
{

    private KLEEmneplanHovedgruppeGruppeEmneEmneVejledningP[] vejledningTekstField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("p", IsNullable = false)]
    public KLEEmneplanHovedgruppeGruppeEmneEmneVejledningP[] VejledningTekst
    {
        get
        {
            return this.vejledningTekstField;
        }
        set
        {
            this.vejledningTekstField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneVejledningP
{

    private string[] ulField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("li", IsNullable = false)]
    public string[] ul
    {
        get
        {
            return this.ulField;
        }
        set
        {
            this.ulField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneRetskildeReference
{

    private string retskildeTitelField;

    private string paragrafEllerKapitelField;

    private string retsinfoAccessionsNrField;

    /// <remarks/>
    public string RetskildeTitel
    {
        get
        {
            return this.retskildeTitelField;
        }
        set
        {
            this.retskildeTitelField = value;
        }
    }

    /// <remarks/>
    public string ParagrafEllerKapitel
    {
        get
        {
            return this.paragrafEllerKapitelField;
        }
        set
        {
            this.paragrafEllerKapitelField = value;
        }
    }

    /// <remarks/>
    public string RetsinfoAccessionsNr
    {
        get
        {
            return this.retsinfoAccessionsNrField;
        }
        set
        {
            this.retsinfoAccessionsNrField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfo
{

    private object[] itemsField;

    private ItemsChoiceType1[] itemsElementNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Historisk", typeof(KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistorisk))]
    [System.Xml.Serialization.XmlElementAttribute("OprettetDato", typeof(System.DateTime), DataType = "date")]
    [System.Xml.Serialization.XmlElementAttribute("RettetDato", typeof(System.DateTime), DataType = "date")]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType1[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistorisk
{

    private KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttet flyttetField;

    private System.DateTime udgaaetDatoField;

    private bool udgaaetDatoFieldSpecified;

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttet Flyttet
    {
        get
        {
            return this.flyttetField;
        }
        set
        {
            this.flyttetField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime UdgaaetDato
    {
        get
        {
            return this.udgaaetDatoField;
        }
        set
        {
            this.udgaaetDatoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UdgaaetDatoSpecified
    {
        get
        {
            return this.udgaaetDatoFieldSpecified;
        }
        set
        {
            this.udgaaetDatoFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttet
{

    private System.DateTime flyttetDatoField;

    private KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttetAfloestAf afloestAfField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime FlyttetDato
    {
        get
        {
            return this.flyttetDatoField;
        }
        set
        {
            this.flyttetDatoField = value;
        }
    }

    /// <remarks/>
    public KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttetAfloestAf AfloestAf
    {
        get
        {
            return this.afloestAfField;
        }
        set
        {
            this.afloestAfField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEEmneplanHovedgruppeGruppeEmneEmneAdministrativInfoHistoriskFlyttetAfloestAf
{

    private decimal gruppeNrField;

    private bool gruppeNrFieldSpecified;

    private string emneNrField;

    /// <remarks/>
    public decimal GruppeNr
    {
        get
        {
            return this.gruppeNrField;
        }
        set
        {
            this.gruppeNrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool GruppeNrSpecified
    {
        get
        {
            return this.gruppeNrFieldSpecified;
        }
        set
        {
            this.gruppeNrFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string EmneNr
    {
        get
        {
            return this.emneNrField;
        }
        set
        {
            this.emneNrField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType1
{

    /// <remarks/>
    Historisk,

    /// <remarks/>
    OprettetDato,

    /// <remarks/>
    RettetDato,
}

