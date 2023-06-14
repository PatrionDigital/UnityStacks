using System.Web;
using Unity.VisualScripting;

[System.Serializable]
public class JSONNFTMetadata
{
    public string token_uri;
    public Metadata metadata;
}

[System.Serializable]
public class Metadata
{
    public int sip;
    public string name;
    public string description;
    public string image;
    public string cached_image;
    public Metadata_Attributes[] attributes;
    public Metadata_Properties properties;
    public Metadata_Localization localization;
}

[System.Serializable]
public class Metadata_Attributes
{
    public string trait_type;
    public string display_type;
    public string value;
}

[System.Serializable]
public class Metadata_Properties
{
    public string collection;
    public string total_supply;
}

[System.Serializable]
public class Metadata_Localization
{
    public string uri;
    public string defaultloc;
    public Metadata_Localization_Locales[] locales;
}

[System.Serializable]
public class Metadata_Localization_Locales
{
    public string code;
}