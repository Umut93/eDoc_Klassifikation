
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("KLE-Handlingsfacetter", Namespace = "", IsNullable = false)]
public partial class KLEHandlingsfacetter
{

    private System.DateTime udgivelsesDatoField;

    private KLEHandlingsfacetterHandlingsfacetKategori[] handlingsfacetKategoriField;

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
    [System.Xml.Serialization.XmlElementAttribute("HandlingsfacetKategori")]
    public KLEHandlingsfacetterHandlingsfacetKategori[] HandlingsfacetKategori
    {
        get
        {
            return this.handlingsfacetKategoriField;
        }
        set
        {
            this.handlingsfacetKategoriField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEHandlingsfacetterHandlingsfacetKategori
{

    private string handlingsfacetKategoriNrField;

    private string handlingsfacetKategoriTitelField;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledning handlingsfacetKategoriVejledningField;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriAdministrativInfo handlingsfacetKategoriAdministrativInfoField;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacet[] handlingsfacetField;

    /// <remarks/>
    public string HandlingsfacetKategoriNr
    {
        get
        {
            return this.handlingsfacetKategoriNrField;
        }
        set
        {
            this.handlingsfacetKategoriNrField = value;
        }
    }

    /// <remarks/>
    public string HandlingsfacetKategoriTitel
    {
        get
        {
            return this.handlingsfacetKategoriTitelField;
        }
        set
        {
            this.handlingsfacetKategoriTitelField = value;
        }
    }

    /// <remarks/>
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledning HandlingsfacetKategoriVejledning
    {
        get
        {
            return this.handlingsfacetKategoriVejledningField;
        }
        set
        {
            this.handlingsfacetKategoriVejledningField = value;
        }
    }

    /// <remarks/>
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriAdministrativInfo HandlingsfacetKategoriAdministrativInfo
    {
        get
        {
            return this.handlingsfacetKategoriAdministrativInfoField;
        }
        set
        {
            this.handlingsfacetKategoriAdministrativInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Handlingsfacet")]
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacet[] Handlingsfacet
    {
        get
        {
            return this.handlingsfacetField;
        }
        set
        {
            this.handlingsfacetField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledning
{

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledningP[] vejledningTekstField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("p", IsNullable = false)]
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledningP[] VejledningTekst
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
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriVejledningP
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
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetKategoriAdministrativInfo
{

    private System.DateTime oprettetDatoField;

    private System.DateTime rettetDatoField;

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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacet
{

    private string handlingsfacetNrField;

    private string handlingsfacetTitelField;

    private string bevaringJaevnfoerArkivlovenField;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledning handlingsfacetVejledningField;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfo handlingsfacetAdministrativInfoField;

    /// <remarks/>
    public string HandlingsfacetNr
    {
        get
        {
            return this.handlingsfacetNrField;
        }
        set
        {
            this.handlingsfacetNrField = value;
        }
    }

    /// <remarks/>
    public string HandlingsfacetTitel
    {
        get
        {
            return this.handlingsfacetTitelField;
        }
        set
        {
            this.handlingsfacetTitelField = value;
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
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledning HandlingsfacetVejledning
    {
        get
        {
            return this.handlingsfacetVejledningField;
        }
        set
        {
            this.handlingsfacetVejledningField = value;
        }
    }

    /// <remarks/>
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfo HandlingsfacetAdministrativInfo
    {
        get
        {
            return this.handlingsfacetAdministrativInfoField;
        }
        set
        {
            this.handlingsfacetAdministrativInfoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledning
{

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledningP[] vejledningTekstField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("p", IsNullable = false)]
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledningP[] VejledningTekst
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
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetVejledningP
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
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfo
{

    private System.DateTime oprettetDatoField;

    private System.DateTime rettetDatoField;

    private bool rettetDatoFieldSpecified;

    private KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfoHistorisk historiskField;

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
    public KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfoHistorisk Historisk
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
public partial class KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacetHandlingsfacetAdministrativInfoHistorisk
{

    private System.DateTime udgaaetDatoField;

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
}

