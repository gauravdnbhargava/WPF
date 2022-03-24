using System.ComponentModel;

namespace MNAIS
{

    public enum ResultsDisplay
    {
        [LocalizableDescription("Decimal", typeof(Resources.MNAISResources))]
        Decimal,
        [LocalizableDescription("Percentage", typeof(Resources.MNAISResources))]
        Percentage
    }

    public enum SimulationPoints
    {
        [LocalizableDescription("FiveHundred", typeof(Resources.MNAISResources))]
        FiveHundred,
        [LocalizableDescription("OneThousand", typeof(Resources.MNAISResources))]
        OneThousand,
        [LocalizableDescription("TwoThousand", typeof(Resources.MNAISResources))]
        TwoThousand
    }

    public enum LicenseFile
    {
        [LocalizableDescription("ChooseOption", typeof(Resources.MNAISResources))]
        ChooseOption,
        [LocalizableDescription("Present", typeof(Resources.MNAISResources))]
        Present,
        [LocalizableDescription("Request", typeof(Resources.MNAISResources))]
        Request        
    }

    public enum AreaSownUnit
    {
        [LocalizableDescription("Ha", typeof(Resources.MNAISResources))]
        Ha,
        [LocalizableDescription("Acres", typeof(Resources.MNAISResources))]
        Acres
    }

    public enum YieldAvailable
    {
        [LocalizableDescription("Yes", typeof(Resources.MNAISResources))]
        Yes,
        [LocalizableDescription("No", typeof(Resources.MNAISResources))]
        No
    }

    public enum Language
    {
        [LocalizableDescription("English", typeof(Resources.MNAISResources))]
        English,
        [LocalizableDescription("Russian", typeof(Resources.MNAISResources))]
        Russian
    }

    public enum DataSource
    {   
        [LocalizableDescription("Yield", typeof(Resources.MNAISResources))]
        Yield        
    }

    public enum Model
    {
        [LocalizableDescription("Normal", typeof(Resources.MNAISResources))]
        Normal,
        [LocalizableDescription("Log Normal", typeof(Resources.MNAISResources))]
        LogNormal,
        [LocalizableDescription("Beta", typeof(Resources.MNAISResources))]
        Beta
    }

    public enum ResultPremiumCharts
    {
        [LocalizableDescription("Mean Loss Ratio", typeof(Resources.MNAISResources))]
        MeanLossRatio,
        [LocalizableDescription("Gross Premium", typeof(Resources.MNAISResources))]
        GrossPremium,
        [LocalizableDescription("Yield Graphs", typeof(Resources.MNAISResources))]
        YieldGraphs
    }

    public enum ResultYieldGraphs
    {
        [LocalizableDescription("Raw", typeof(Resources.MNAISResources))]
        Raw,
        [LocalizableDescription("Detrended", typeof(Resources.MNAISResources))]
        Detrended
    }

    public enum ErrorType
    {
        [Description("Error")]
        Error,
        [Description("Warning")]
        Warning,
        [Description("Debug")]
        Debug
    }

    public enum SignificanceForTrending
    {
        [LocalizableDescription("DetrendAll", typeof(Resources.MNAISResources))]
        DetrendAll,        
        [LocalizableDescription("90 %", typeof(Resources.MNAISResources))]
        Ninety,
        [LocalizableDescription("95 %", typeof(Resources.MNAISResources))]
        Ninetyfive,
        [LocalizableDescription("99 %", typeof(Resources.MNAISResources))]
        NientyNine,
        [LocalizableDescription("Detrend None", typeof(Resources.MNAISResources))]
        DetrendNone
    }

    public enum DataProcessed
    {
        [LocalizableDescription("Raw", typeof(Resources.MNAISResources))]
        Raw,
        [LocalizableDescription("Detrended", typeof(Resources.MNAISResources))]
        Detrended,
        [LocalizableDescription("Moving Average", typeof(Resources.MNAISResources))]
        MovingAverage
    }

    public enum DataGaps
    {
        [LocalizableDescription("Filled", typeof(Resources.MNAISResources))]
        Filled,
        [LocalizableDescription("Not Filled", typeof(Resources.MNAISResources))]
        NotFilled
    }
    
    public enum Calamity
    {
        [LocalizableDescription("Included", typeof(Resources.MNAISResources))]
        Included,
        [LocalizableDescription("Excluded", typeof(Resources.MNAISResources))]
        Excluded
    }

    public enum StdDev
    {
        [LocalizableDescription("Sum", typeof(Resources.MNAISResources))]
        Sum,
        [LocalizableDescription("Correlation", typeof(Resources.MNAISResources))]
        Correlation
    }

    public enum SimulationDistribution
    {
        [LocalizableDescription("Normal", typeof(Resources.MNAISResources))]
        Normal,
        [LocalizableDescription("Log Normal", typeof(Resources.MNAISResources))]
        LogNormal,
        [LocalizableDescription("Beta", typeof(Resources.MNAISResources))]
        Beta
    }

    public enum ExtremeBoundMethod
    {
        [LocalizableDescription("Sigma", typeof(Resources.MNAISResources))]
        Sigma,        
        [LocalizableDescription("% Extreme", typeof(Resources.MNAISResources))]
        PercentExtreme
    }

    public enum PayoutAggregationModel
    {
        [LocalizableDescription("Normal", typeof(Resources.MNAISResources))]
        Normal,
        [LocalizableDescription("Log Normal", typeof(Resources.MNAISResources))]
        LogNormal,
        [LocalizableDescription("Beta", typeof(Resources.MNAISResources))]
        Beta
    }

    public enum HistoricalMethod
    {
        [LocalizableDescription("Historical", typeof(Resources.MNAISResources))]
        Historical
        //[LocalizableDescription("Yield Correlation", typeof(Resources.MNAISResources))]
        //YieldCorrelation,        
        //[LocalizableDescription("Payout Correlation", typeof(Resources.MNAISResources))]
        //PayoutCorrelation,
        //[LocalizableDescription("Alpha", typeof(Resources.MNAISResources))]
        //Alpha
    }

    public enum SimulationMethod
    {   
        [LocalizableDescription("Yield Correlation", typeof(Resources.MNAISResources))]
        YieldCorrelation,        
        [LocalizableDescription("Payout Correlation", typeof(Resources.MNAISResources))]
        PayoutCorrelation
        //[LocalizableDescription("Alpha", typeof(Resources.MNAISResources))]
        //Alpha
    }

    public enum ReturnOnCapital
    {
        [LocalizableDescription("Calculated", typeof(Resources.MNAISResources))]
        Calculated,
        [LocalizableDescription("User Specified", typeof(Resources.MNAISResources))]
        UserSpecified
    }

    public enum HistoricalPML
    {
        [LocalizableDescription("Maximum", typeof(Resources.MNAISResources))]
        Maximum,
        [LocalizableDescription("Return Period", typeof(Resources.MNAISResources))]
        ReturnPeriod
    }

    public enum DataLoading
    {
        [LocalizableDescription("Calculated", typeof(Resources.MNAISResources))]
        Calculated,
        [LocalizableDescription("Straight", typeof(Resources.MNAISResources))]
        Straight,
        [LocalizableDescription("Proportional", typeof(Resources.MNAISResources))]
        Proportional
    }

    public enum Franchise
    {
        [LocalizableDescription("Franchise", typeof(Resources.MNAISResources))]
        Franchise,
        [LocalizableDescription("Deductible", typeof(Resources.MNAISResources))]
        Deductible
    }
      
    public enum Season
    {
        [LocalizableDescription("Rabi", typeof(Resources.MNAISResources))]
        Rabi,
        [LocalizableDescription("Kharif", typeof(Resources.MNAISResources))]
        Kharif
    }

    public enum IndemnityLevel
    {
        [LocalizableDescription("70 %", typeof(Resources.MNAISResources))]
        Seventy,
        [LocalizableDescription("80 %", typeof(Resources.MNAISResources))]
        Eighty,
        [LocalizableDescription("90 %", typeof(Resources.MNAISResources))]
        Ninety
    }

    public enum Crop
    {        
        [LocalizableDescription("All Crops", typeof(Resources.MNAISResources))]
        AllCrops,        
        [LocalizableDescription("Aman Paddy", typeof(Resources.MNAISResources))]
        AmanPaddy,        
        [LocalizableDescription("Arhar", typeof(Resources.MNAISResources))]
        Arhar,        
        [LocalizableDescription("Bajra", typeof(Resources.MNAISResources))]
        Bajra,        
        [LocalizableDescription("Barley", typeof(Resources.MNAISResources))]
        Barley,        
        [LocalizableDescription("Bhindi", typeof(Resources.MNAISResources))]
        Bhindi,        
        [LocalizableDescription("Boro Paddy", typeof(Resources.MNAISResources))]
        BoroPaddy,        
        [LocalizableDescription("Brinjal", typeof(Resources.MNAISResources))]
        Brinjal,        
        [LocalizableDescription("Chillies", typeof(Resources.MNAISResources))]
        Chillies,        
        [LocalizableDescription("Coriander", typeof(Resources.MNAISResources))]
        Coriander,        
        [LocalizableDescription("Cotton", typeof(Resources.MNAISResources))]
        Cotton,        
        [LocalizableDescription("Cotton Irrigated", typeof(Resources.MNAISResources))]
        CottonIrrigated,        
        [LocalizableDescription("Cotton Rainfed", typeof(Resources.MNAISResources))]
        CottonRainfed,        
        [LocalizableDescription("Fenugreek (Methi)", typeof(Resources.MNAISResources))]
        Fenugreek,        
        [LocalizableDescription("Gram", typeof(Resources.MNAISResources))]
        Gram,        
        [LocalizableDescription("Groundnut", typeof(Resources.MNAISResources))]
        Groundnut,        
        [LocalizableDescription("Maize", typeof(Resources.MNAISResources))]
        Maize,        
        [LocalizableDescription("Millets (other than Maize)", typeof(Resources.MNAISResources))]
        Millets,        
        [LocalizableDescription("Mustard", typeof(Resources.MNAISResources))]
        Mustard,        
        [LocalizableDescription("Onion", typeof(Resources.MNAISResources))]
        Onion,        
        [LocalizableDescription("Other Millets", typeof(Resources.MNAISResources))]
        OtherMillets,        
        [LocalizableDescription("Others", typeof(Resources.MNAISResources))]
        Others,        
        [LocalizableDescription("Paddy", typeof(Resources.MNAISResources))]
        Paddy,        
        [LocalizableDescription("Paddy II", typeof(Resources.MNAISResources))]
        PaddyII,        
        [LocalizableDescription("Potato", typeof(Resources.MNAISResources))]
        Potato,        
        [LocalizableDescription("Pulses", typeof(Resources.MNAISResources))]
        Pulses,        
        [LocalizableDescription("Taramira", typeof(Resources.MNAISResources))]
        Taramira,        
        [LocalizableDescription("Tomato", typeof(Resources.MNAISResources))]
        Tomato,        
        [LocalizableDescription("Urad", typeof(Resources.MNAISResources))]
        Urad,        
        [LocalizableDescription("Vegetables", typeof(Resources.MNAISResources))]
        Vegetables,        
        [LocalizableDescription("Wheat", typeof(Resources.MNAISResources))]
        Wheat,        
        [LocalizableDescription("Apple", typeof(Resources.MNAISResources))]
        Apple,        
        [LocalizableDescription("Banana", typeof(Resources.MNAISResources))]
        Banana,        
        [LocalizableDescription("Lime/Lemon", typeof(Resources.MNAISResources))]
        Lemon,        
        [LocalizableDescription("Mosambi", typeof(Resources.MNAISResources))]
        Mosambi,        
        [LocalizableDescription("Orange", typeof(Resources.MNAISResources))]
        Orange,        
        [LocalizableDescription("Grapes", typeof(Resources.MNAISResources))]
        Grapes,        
        [LocalizableDescription("Guava", typeof(Resources.MNAISResources))]
        Guava,        
        [LocalizableDescription("Litchi", typeof(Resources.MNAISResources))]
        Litchi,        
        [LocalizableDescription("Mango", typeof(Resources.MNAISResources))]
        Mango,        
        [LocalizableDescription("Papaya", typeof(Resources.MNAISResources))]
        Papaya,        
        [LocalizableDescription("Pine Apple", typeof(Resources.MNAISResources))]
        Pineapple,        
        [LocalizableDescription("Pomegranate", typeof(Resources.MNAISResources))]
        Pomegranate,        
        [LocalizableDescription("Sapota", typeof(Resources.MNAISResources))]
        Sapota,        
        [LocalizableDescription("Keenu", typeof(Resources.MNAISResources))]
        Keenu,        
        [LocalizableDescription("Cabbage", typeof(Resources.MNAISResources))]
        Cabbage,        
        [LocalizableDescription("Cauliflower", typeof(Resources.MNAISResources))]
        Cauliflower,        
        [LocalizableDescription("Peas", typeof(Resources.MNAISResources))]
        Peas,        
        [LocalizableDescription("Sweet Potato", typeof(Resources.MNAISResources))]
        SweetPotato,        
        [LocalizableDescription("Tapioca", typeof(Resources.MNAISResources))]
        Tapioca,        
        [LocalizableDescription("Arecanut", typeof(Resources.MNAISResources))]
        Arecanut,        
        [LocalizableDescription("Cashewnut", typeof(Resources.MNAISResources))]
        Cashewnut,        
        [LocalizableDescription("Coconut", typeof(Resources.MNAISResources))]
        Coconut,        
        [LocalizableDescription("Cocoa", typeof(Resources.MNAISResources))]
        Cocoa,        
        [LocalizableDescription("Jackfruit", typeof(Resources.MNAISResources))]
        Jackfruit,        
        [LocalizableDescription("Anise", typeof(Resources.MNAISResources))]
        Anise,        
        [LocalizableDescription("Cardamom", typeof(Resources.MNAISResources))]
        Cardamom,        
        [LocalizableDescription("Castor Oil seed", typeof(Resources.MNAISResources))]
        CastorOilseed,        
        [LocalizableDescription("Chick Peas", typeof(Resources.MNAISResources))]
        ChickPeas,        
        [LocalizableDescription("Dry Beans", typeof(Resources.MNAISResources))]
        DryBeans,        
        [LocalizableDescription("Peppers", typeof(Resources.MNAISResources))]
        Peppers,        
        [LocalizableDescription("Fennel", typeof(Resources.MNAISResources))]
        Fennel,        
        [LocalizableDescription("Garlic", typeof(Resources.MNAISResources))]
        Garlic,        
        [LocalizableDescription("Ginger", typeof(Resources.MNAISResources))]
        Ginger,        
        [LocalizableDescription("Gourds", typeof(Resources.MNAISResources))]
        Gourds,        
        [LocalizableDescription("Green Peas", typeof(Resources.MNAISResources))]
        GreenPeas,        
        [LocalizableDescription("Jute", typeof(Resources.MNAISResources))]
        Jute,        
        [LocalizableDescription("Lentil", typeof(Resources.MNAISResources))]
        Lentil,        
        [LocalizableDescription("Nutmeg And Mace", typeof(Resources.MNAISResources))]
        NutmegAndMace,        
        [LocalizableDescription("Horse Gram", typeof(Resources.MNAISResources))]
        HorseGram,        
        [LocalizableDescription("Sunflower", typeof(Resources.MNAISResources))]
        Sunflower,        
        [LocalizableDescription("Guar", typeof(Resources.MNAISResources))]
        Guar,        
        [LocalizableDescription("Pigeon Peas", typeof(Resources.MNAISResources))]
        PigeonPeas,        
        [LocalizableDescription("Pumpkin", typeof(Resources.MNAISResources))]
        Pumpkin,        
        [LocalizableDescription("Rape seed", typeof(Resources.MNAISResources))]
        Rapeseed,        
        [LocalizableDescription("Safflower Seeds", typeof(Resources.MNAISResources))]
        SafflowerSeeds,        
        [LocalizableDescription("Sesame Seeds", typeof(Resources.MNAISResources))]
        SesameSeeds,        
        [LocalizableDescription("Jowar", typeof(Resources.MNAISResources))]
        Jowar,        
        [LocalizableDescription("Spices", typeof(Resources.MNAISResources))]
        Spices,        
        [LocalizableDescription("Sugar Cane", typeof(Resources.MNAISResources))]
        Sugarcane,        
        [LocalizableDescription("Tea", typeof(Resources.MNAISResources))]
        Tea,        
        [LocalizableDescription("Tobacco", typeof(Resources.MNAISResources))]
        Tobacco,        
        [LocalizableDescription("Turmeric", typeof(Resources.MNAISResources))]
        Turmeric,        
        [LocalizableDescription("Ragi", typeof(Resources.MNAISResources))]
        Ragi,        
        [LocalizableDescription("Moong", typeof(Resources.MNAISResources))]
        Moong,        
        [LocalizableDescription("Masoor", typeof(Resources.MNAISResources))]
        Masoor,        
        [LocalizableDescription("Moth", typeof(Resources.MNAISResources))]
        Moth,        
        [LocalizableDescription("Linseed", typeof(Resources.MNAISResources))]
        Linseed,        
        [LocalizableDescription("Soyabean", typeof(Resources.MNAISResources))]
        Soyabean,        
        [LocalizableDescription("Isabgol", typeof(Resources.MNAISResources))]
        Isabgol,        
        [LocalizableDescription("Til", typeof(Resources.MNAISResources))]
        Til,        
        [LocalizableDescription("Cowpea", typeof(Resources.MNAISResources))]
        Cowpea,        
        [LocalizableDescription("Cumin", typeof(Resources.MNAISResources))]
        Cumin
    }

    public enum State
    {        
        [LocalizableDescription("Uttar Pradesh", typeof(Resources.MNAISResources))]
        UttarPradesh,        
        [LocalizableDescription("Maharashtra", typeof(Resources.MNAISResources))]
        Maharashtra,        
        [LocalizableDescription("Bihar", typeof(Resources.MNAISResources))]
        Bihar,        
        [LocalizableDescription("West Bengal", typeof(Resources.MNAISResources))]
        WestBengal,        
        [LocalizableDescription("Andhra Pradesh", typeof(Resources.MNAISResources))]
        AndhraPradesh,        
        [LocalizableDescription("Madhya Pradesh", typeof(Resources.MNAISResources))]
        MadhyaPradesh,        
        [LocalizableDescription("Tamil Nadu", typeof(Resources.MNAISResources))]
        TamilNadu,        
        [LocalizableDescription("Rajasthan", typeof(Resources.MNAISResources))]
        Rajasthan,        
        [LocalizableDescription("Karnataka", typeof(Resources.MNAISResources))]
        Karnataka,        
        [LocalizableDescription("Gujarat", typeof(Resources.MNAISResources))]
        Gujarat,        
        [LocalizableDescription("Odisha", typeof(Resources.MNAISResources))]
        Odisha,        
        [LocalizableDescription("Kerala", typeof(Resources.MNAISResources))]
        Kerala,        
        [LocalizableDescription("Jharkhand", typeof(Resources.MNAISResources))]
        Jharkhand,        
        [LocalizableDescription("Assam", typeof(Resources.MNAISResources))]
        Assam,        
        [LocalizableDescription("Punjab", typeof(Resources.MNAISResources))]
        Punjab,        
        [LocalizableDescription("Haryana", typeof(Resources.MNAISResources))]
        Haryana,        
        [LocalizableDescription("Chhattisgarh", typeof(Resources.MNAISResources))]
        Chhattisgarh,        
        [LocalizableDescription("Jammu and Kashmir", typeof(Resources.MNAISResources))]
        JammuandKashmir,        
        [LocalizableDescription("Uttarakhand", typeof(Resources.MNAISResources))]
        Uttarakhand,        
        [LocalizableDescription("Himachal Pradesh", typeof(Resources.MNAISResources))]
        HimachalPradesh,        
        [LocalizableDescription("Tripura", typeof(Resources.MNAISResources))]
        Tripura,        
        [LocalizableDescription("Meghalaya", typeof(Resources.MNAISResources))]
        Meghalaya,        
        [LocalizableDescription("Manipur", typeof(Resources.MNAISResources))]
        Manipur,        
        [LocalizableDescription("Nagaland", typeof(Resources.MNAISResources))]
        Nagaland,        
        [LocalizableDescription("Goa", typeof(Resources.MNAISResources))]
        Goa,        
        [LocalizableDescription("Arunachal Pradesh", typeof(Resources.MNAISResources))]
        ArunachalPradesh,        
        [LocalizableDescription("Mizoram", typeof(Resources.MNAISResources))]
        Mizoram,        
        [LocalizableDescription("Sikkim", typeof(Resources.MNAISResources))]
        Sikkim,
        [LocalizableDescription("Delhi", typeof(Resources.MNAISResources))]
        Delhi,        
        [LocalizableDescription("Puducherry", typeof(Resources.MNAISResources))]
        Puducherry,        
        [LocalizableDescription("Chandigarh", typeof(Resources.MNAISResources))]
        Chandigarh,        
        [LocalizableDescription("Andaman and Nicobar Islands", typeof(Resources.MNAISResources))]
        AndamanandNicobarIslands,        
        [LocalizableDescription("Dadra and Nagar Haveli", typeof(Resources.MNAISResources))]
        DadraandNagarHaveli,        
        [LocalizableDescription("Daman and Diu", typeof(Resources.MNAISResources))]
        DamanandDiu,        
        [LocalizableDescription("Lakshadweep", typeof(Resources.MNAISResources))]
        Lakshadweep
    }

    #region District

    public enum AndamanandNicobarIslands
    {
        [Description("Mayabunder")]
        Mayabunder,
        [Description("Port Blair")]
        PortBlair,
        [Description("Car Nicobar")]
        CarNicobar
    }

    public enum AndhraPradesh
    {
        [Description("Adilabad")]
        Adilabad,
        [Description("Anantapur")]
        Anantapur,
        [Description("Chittoor")]
        Chittoor,
        [Description("East Godavari")]
        EastGodavari,
        [Description("Guntur")]
        Guntur,
        [Description("Hyderabad")]
        Hyderabad,
        [Description("YSR district")]
        YSRdistrict,
        [Description("Karimnagar")]
        Karimnagar,
        [Description("Khammam")]
        Khammam,
        [Description("Krishna")]
        Krishna,
        [Description("Kurnool")]
        Kurnool,
        [Description("Mahbubnagar")]
        Mahbubnagar,
        [Description("Medak")]
        Medak,
        [Description("Nalgonda")]
        Nalgonda,
        [Description("Nellore")]
        Nellore,
        [Description("Nizamabad")]
        Nizamabad,
        [Description("Prakasam")]
        Prakasam,
        [Description("Rangareddi")]
        Rangareddi,
        [Description("Srikakulam")]
        Srikakulam,
        [Description("Vishakhapatnam")]
        Vishakhapatnam,
        [Description("Vizianagaram")]
        Vizianagaram,
        [Description("Warangal")]
        Warangal,
        [Description("West Godavari")]
        WestGodavari
    }

    public enum ArunachalPradesh
    {
        [Description("Anjaw")]
        Anjaw,
        [Description("Changlang")]
        Changlang,
        [Description("East Kameng")]
        EastKameng,
        [Description("East Siang")]
        EastSiang,
        [Description("Kurung Kumey")]
        KurungKumey,
        [Description("Lohit")]
        Lohit,
        [Description("Lower Dibang Valley")]
        LowerDibangValley,
        [Description("Lower Subansiri")]
        LowerSubansiri,
        [Description("Papum Pare")]
        PapumPare,
        [Description("Tawang")]
        Tawang,
        [Description("Tirap")]
        Tirap,
        [Description("Dibang Valley")]
        DibangValley,
        [Description("Upper Siang")]
        UpperSiang,
        [Description("Upper Subansiri")]
        UpperSubansiri,
        [Description("West Kameng")]
        WestKameng,
        [Description("West Siang")]
        WestSiang
    }

    public enum Assam
    {
        [Description("Anglong")]
        Anglong,
        [Description("Baksa")]
        Baksa,
        [Description("Barpeta")]
        Barpeta,
        [Description("Bongaigaon")]
        Bongaigaon,
        [Description("Cachar")]
        Cachar,
        [Description("Chirang")]
        Chirang,
        [Description("Darrang")]
        Darrang,
        [Description("Dhemaji")]
        Dhemaji,
        [Description("Dhubri")]
        Dhubri,
        [Description("Dibrugarh")]
        Dibrugarh,
        [Description("Goalpara")]
        Goalpara,
        [Description("Golaghat")]
        Golaghat,
        [Description("Hailakandi")]
        Hailakandi,
        [Description("Jorhat")]
        Jorhat,
        [Description("Kamrup Metropolitan")]
        KamrupMetropolitan,
        [Description("Karbi Anglong")]
        KarbiAnglong,
        [Description("Karimganj")]
        Karimganj,
        [Description("Kokrajhar")]
        Kokrajhar,
        [Description("Lakhimpur")]
        Lakhimpur,
        [Description("Marigaon")]
        Marigaon,
        [Description("Nagaon")]
        Nagaon,
        [Description("Nalbari")]
        Nalbari,
        [Description("North Cachar Hills")]
        NorthCacharHills,
        [Description("Sibsagar")]
        Sibsagar,
        [Description("Sonitpur")]
        Sonitpur,
        [Description("Tinsukia")]
        Tinsukia,
        [Description("Udalguri")]
        Udalguri
    }

    public enum Bihar
    {
        [Description("Araria")]
        Araria,
        [Description("Arwal")]
        Arwal,
        [Description("Aurangabad")]
        Aurangabad,
        [Description("Banka")]
        Banka,
        [Description("Begusarai")]
        Begusarai,
        [Description("Bhagalpur")]
        Bhagalpur,
        [Description("Bhojpur")]
        Bhojpur,
        [Description("Buxar")]
        Buxar,
        [Description("Darbhanga")]
        Darbhanga,
        [Description("East Champaran")]
        EastChamparan,
        [Description("Gaya")]
        Gaya,
        [Description("Gopalganj")]
        Gopalganj,
        [Description("Jamui")]
        Jamui,
        [Description("Jehanabad")]
        Jehanabad,
        [Description("Khagaria")]
        Khagaria,
        [Description("Kishanganj")]
        Kishanganj,
        [Description("Kaimur")]
        Kaimur,
        [Description("Katihar")]
        Katihar,
        [Description("Lakhisarai")]
        Lakhisarai,
        [Description("Madhubani")]
        Madhubani,
        [Description("Munger")]
        Munger,
        [Description("Madhepura")]
        Madhepura,
        [Description("Muzaffarpur")]
        Muzaffarpur,
        [Description("Nalanda")]
        Nalanda,
        [Description("Nawada")]
        Nawada,
        [Description("Patna")]
        Patna,
        [Description("Purnia")]
        Purnia,
        [Description("Rohtas")]
        Rohtas,
        [Description("Saharsa")]
        Saharsa,
        [Description("Samastipur")]
        Samastipur,
        [Description("Sheohar")]
        Sheohar,
        [Description("Sheikhpura")]
        Sheikhpura,
        [Description("Saran")]
        Saran,
        [Description("Sitamarhi")]
        Sitamarhi,
        [Description("Supaul")]
        Supaul,
        [Description("Siwan")]
        Siwan,
        [Description("Vaishali")]
        Vaishali,
        [Description("West Champaran")]
        WestChamparan
    }

    public enum Chandigarh
    {
        [Description("Chandigarh")]
        Chandigarh
    }

    public enum Chattisgarh
    {
        [Description("Bastar")]
        Bastar,
        [Description("Bijapur")]
        Bijapur,
        [Description("Bilaspur")]
        Bilaspur,
        [Description("Dantewada")]
        Dantewada,
        [Description("Dhamtari")]
        Dhamtari,
        [Description("Durg")]
        Durg,
        [Description("Jashpur")]
        Jashpur,
        [Description("JanjgirChampa")]
        JanjgirChampa,
        [Description("Korba")]
        Korba,
        [Description("Koriya")]
        Koriya,
        [Description("Kanker")]
        Kanker,
        [Description("Kawardha")]
        Kawardha,
        [Description("Mahasamund")]
        Mahasamund,
        [Description("Narayanpur")]
        Narayanpur,
        [Description("Raigarh")]
        Raigarh,
        [Description("Rajnandgaon")]
        Rajnandgaon,
        [Description("Raipur")]
        Raipur,
        [Description("Surguja")]
        Surguja
    }

    public enum DadraandNagarHaveli
    {
        [Description("Dadra and Nagar Haveli")]
        DadraandNagarHaveli
    }

    public enum DamanandDiu
    {
        [Description("Daman")]
        Daman,
        [Description("Diu")]
        Diu
    }

    public enum Goa
    {
        [Description("North Goa")]
        NorthGoa,
        [Description("South Goa")]
        SouthGoa
    }

    public enum Gujrat
    {
        [Description("Ahmedabad")]
        Ahmedabad,
        [Description("Amreli District")]
        AmreliDistrict,
        [Description("Anand")]
        Anand,
        [Description("Banaskantha")]
        Banaskantha,
        [Description("Bharuch")]
        Bharuch,
        [Description("Bhavnagar")]
        Bhavnagar,
        [Description("Dahod")]
        Dahod,
        [Description("TheDangs")]
        TheDangs,
        [Description("Gandhinagar")]
        Gandhinagar,
        [Description("Jamnagar")]
        Jamnagar,
        [Description("Junagadh")]
        Junagadh,
        [Description("Kutch")]
        Kutch,
        [Description("Kheda")]
        Kheda,
        [Description("Mehsana")]
        Mehsana,
        [Description("Narmada")]
        Narmada,
        [Description("Navsari")]
        Navsari,
        [Description("Patan")]
        Patan,
        [Description("Panchmahal")]
        Panchmahal,
        [Description("Porbandar")]
        Porbandar,
        [Description("Rajkot")]
        Rajkot,
        [Description("Sabarkantha")]
        Sabarkantha,
        [Description("Surendranagar")]
        Surendranagar,
        [Description("Surat")]
        Surat,
        [Description("Tapi")]
        Tapi,
        [Description("Vadodara")]
        Vadodara,
        [Description("Valsad")]
        Valsad
    }

    public enum Haryana
    {
        [Description("Ambala")]
        Ambala,
        [Description("Bhiwani")]
        Bhiwani,
        [Description("Faridabad")]
        Faridabad,
        [Description("Fatehabad")]
        Fatehabad,
        [Description("Gurgaon")]
        Gurgaon,
        [Description("Hissar")]
        Hissar,
        [Description("Jhajjar")]
        Jhajjar,
        [Description("Jind")]
        Jind,
        [Description("Karnal")]
        Karnal,
        [Description("Kaithal")]
        Kaithal,
        [Description("Kurukshetra")]
        Kurukshetra,
        [Description("Mahendragarh")]
        Mahendragarh,
        [Description("Mewat")]
        Mewat,
        [Description("Panchkula")]
        Panchkula,
        [Description("Panipat")]
        Panipat,
        [Description("Rewari")]
        Rewari,
        [Description("Rohtak")]
        Rohtak,
        [Description("Sirsa")]
        Sirsa,
        [Description("Sonepat")]
        Sonepat,
        [Description("YamunaNagar")]
        YamunaNagar,
        [Description("Palwal")]
        Palwal
    }

    public enum HimachalPradesh
    {
        [Description("Bilaspur")]
        Bilaspur,
        [Description("Chamba")]
        Chamba,
        [Description("Hamirpur")]
        Hamirpur,
        [Description("Kangra")]
        Kangra,
        [Description("Kinnaur")]
        Kinnaur,
        [Description("Kulu")]
        Kulu,
        [Description("Lahaul and Spiti")]
        LahaulandSpiti,
        [Description("Mandi")]
        Mandi,
        [Description("Shimla")]
        Shimla,
        [Description("Sirmaur")]
        Sirmaur,
        [Description("Solan")]
        Solan,
        [Description("Una")]
        Una
    }

    public enum JammuandKashmir
    {
        [Description("Anantnag")]
        Anantnag,
        [Description("Badgam")]
        Badgam,
        [Description("Bandipore")]
        Bandipore,
        [Description("Baramula")]
        Baramula,
        [Description("Doda")]
        Doda,
        [Description("Ganderbal")]
        Ganderbal,
        [Description("Jammu")]
        Jammu,
        [Description("Kargil")]
        Kargil,
        [Description("Kathua")]
        Kathua,
        [Description("Kishtwar")]
        Kishtwar,
        [Description("Kulgam")]
        Kulgam,
        [Description("Kupwara")]
        Kupwara,
        [Description("Leh")]
        Leh,
        [Description("Poonch")]
        Poonch,
        [Description("Pulwama")]
        Pulwama,
        [Description("Rajauri")]
        Rajauri,
        [Description("Ramban")]
        Ramban,
        [Description("Srinagar")]
        Srinagar,
        [Description("Samba")]
        Samba,
        [Description("Shopian")]
        Shopian,
        [Description("Udhampur")]
        Udhampur
    }

    public enum Jharkhand
    {
        [Description("Bokaro")]
        Bokaro,
        [Description("Chatra")]
        Chatra,
        [Description("Deoghar")]
        Deoghar,
        [Description("Dhanbad")]
        Dhanbad,
        [Description("Dumka")]
        Dumka,
        [Description("East Singhbhum")]
        EastSinghbhum,
        [Description("Garhwa")]
        Garhwa,
        [Description("Giridih")]
        Giridih,
        [Description("Godda")]
        Godda,
        [Description("Gumla")]
        Gumla,
        [Description("Hazaribag")]
        Hazaribag,
        [Description("Jamtara")]
        Jamtara,
        [Description("Khunti")]
        Khunti,
        [Description("Koderma")]
        Koderma,
        [Description("Latehar")]
        Latehar,
        [Description("Lohardaga")]
        Lohardaga,
        [Description("Pakur")]
        Pakur,
        [Description("Palamu")]
        Palamu,
        [Description("Ramgarh")]
        Ramgarh,
        [Description("Ranchi")]
        Ranchi,
        [Description("Sahibganj")]
        Sahibganj,
        [Description("Seraikela Kharsawan")]
        SeraikelaKharsawan,
        [Description("West Singhbhum")]
        WestSinghbhum
    }

    public enum Karnataka
    {
        [Description("Bagalkot")]
        Bagalkot,
        [Description("Bangalore Rural District")]
        BangaloreRuralDistrict,
        [Description("Bangalore Urban District")]
        BangaloreUrbandistrict,
        [Description("Belgaum")]
        Belgaum,
        [Description("Bellary")]
        Bellary,
        [Description("Bidar")]
        Bidar,
        [Description("Bijapur")]
        Bijapur,
        [Description("Chamarajnagar")]
        Chamarajnagar,
        [Description("Chikballapur")]
        Chikballapur,
        [Description("Chikmagalur")]
        Chikmagalur,
        [Description("Chitradurga")]
        Chitradurga,
        [Description("DakshinaKannada")]
        DakshinaKannada,
        [Description("Davanagere")]
        Davanagere,
        [Description("Dharwad")]
        Dharwad,
        [Description("Gadag")]
        Gadag,
        [Description("Gulbarga")]
        Gulbarga,
        [Description("Hassan")]
        Hassan,
        [Description("Haveri District")]
        HaveriDistrict,
        [Description("Kodagu")]
        Kodagu,
        [Description("Kolar")]
        Kolar,
        [Description("Koppal")]
        Koppal,
        [Description("Mandya")]
        Mandya,
        [Description("Mysore")]
        Mysore,
        [Description("Raichur")]
        Raichur,
        [Description("Ramanagara")]
        Ramanagara,
        [Description("Shimoga")]
        Shimoga,
        [Description("Tumkur")]
        Tumkur,
        [Description("Udupi")]
        Udupi,
        [Description("UttaraKannada")]
        UttaraKannada,
        [Description("Yadgir")]
        Yadgir
    }

    public enum Kerala
    {
        [Description("Alappuzha")]
        Alappuzha,
        [Description("Ernakulam")]
        Ernakulam,
        [Description("Idukki")]
        Idukki,
        [Description("Kannur")]
        Kannur,
        [Description("Kasaragod")]
        Kasaragod,
        [Description("Kollam")]
        Kollam,
        [Description("Kottayam")]
        Kottayam,
        [Description("Kozhikode")]
        Kozhikode,
        [Description("Malappuram")]
        Malappuram,
        [Description("Palakkad")]
        Palakkad,
        [Description("Pathanamthitta")]
        Pathanamthitta,
        [Description("Thiruvananthapuram")]
        Thiruvananthapuram,
        [Description("Thrissur")]
        Thrissur,
        [Description("Wayanad")]
        Wayanad
    }

    public enum Lakshadweep
    {
        [Description("Lakshadweep")]
        Lakshadweep
    }

    public enum MadhyaPradesh
    {
        [Description("Alirajpur")]
        Alirajpur,
        [Description("Anuppur")]
        Anuppur,
        [Description("AshokNagar")]
        AshokNagar,
        [Description("Balaghat")]
        Balaghat,
        [Description("Barwani")]
        Barwani,
        [Description("Betul")]
        Betul,
        [Description("Bhind")]
        Bhind,
        [Description("Bhopal")]
        Bhopal,
        [Description("Burhanpur")]
        Burhanpur,
        [Description("Chhatarpur")]
        Chhatarpur,
        [Description("Chhindwara")]
        Chhindwara,
        [Description("Damoh")]
        Damoh,
        [Description("Datia")]
        Datia,
        [Description("Dewas")]
        Dewas,
        [Description("Dhar")]
        Dhar,
        [Description("Dindori")]
        Dindori,
        [Description("Guna")]
        Guna,
        [Description("Gwalior")]
        Gwalior,
        [Description("Harda")]
        Harda,
        [Description("Hoshangabad")]
        Hoshangabad,
        [Description("Indore")]
        Indore,
        [Description("Jabalpur")]
        Jabalpur,
        [Description("Jhabua")]
        Jhabua,
        [Description("Katni")]
        Katni,
        [Description("Khandwa East Nimar")]
        KhandwaEastNimar,
        [Description("Khargone West Nimar")]
        KhargoneWestNimar,
        [Description("Mandla")]
        Mandla,
        [Description("Mandsaur")]
        Mandsaur,
        [Description("Morena")]
        Morena,
        [Description("Narsinghpur")]
        Narsinghpur,
        [Description("Neemuch")]
        Neemuch,
        [Description("Panna")]
        Panna,
        [Description("Raisen")]
        Raisen,
        [Description("Rajgarh")]
        Rajgarh,
        [Description("Ratlam")]
        Ratlam,
        [Description("Rewa")]
        Rewa,
        [Description("Sagar")]
        Sagar,
        [Description("Satna")]
        Satna,
        [Description("Sehore")]
        Sehore,
        [Description("Seoni")]
        Seoni,
        [Description("Shahdol")]
        Shahdol,
        [Description("Shajapur")]
        Shajapur,
        [Description("Sheopur")]
        Sheopur,
        [Description("Shivpuri")]
        Shivpuri,
        [Description("Sidhi")]
        Sidhi,
        [Description("Singrauli")]
        Singrauli,
        [Description("Tikamgarh")]
        Tikamgarh,
        [Description("Ujjain")]
        Ujjain,
        [Description("Umaria")]
        Umaria,
        [Description("Vidisha")]
        Vidisha
    }

    public enum Maharashtra
    {
        [Description("Ahmednagar")]
        Ahmednagar,
        [Description("Akola")]
        Akola,
        [Description("Amravati")]
        Amravati,
        [Description("Aurangabad")]
        Aurangabad,
        [Description("Beed")]
        Beed,
        [Description("Bhandara")]
        Bhandara,
        [Description("Buldhana")]
        Buldhana,
        [Description("Chandrapur")]
        Chandrapur,
        [Description("Dhule")]
        Dhule,
        [Description("Gadchiroli")]
        Gadchiroli,
        [Description("Gondia")]
        Gondia,
        [Description("Hingoli")]
        Hingoli,
        [Description("Jalgaon")]
        Jalgaon,
        [Description("Jalna")]
        Jalna,
        [Description("Kolhapur")]
        Kolhapur,
        [Description("Latur")]
        Latur,
        [Description("Mumbai City")]
        MumbaiCity,
        [Description("Mumbai Suburban")]
        Mumbaisuburban,
        [Description("Nagpur")]
        Nagpur,
        [Description("Nanded")]
        Nanded,
        [Description("Nandurbar")]
        Nandurbar,
        [Description("Nashik")]
        Nashik,
        [Description("Osmanabad")]
        Osmanabad,
        [Description("Parbhani")]
        Parbhani,
        [Description("Pune")]
        Pune,
        [Description("Raigad")]
        Raigad,
        [Description("Ratnagiri")]
        Ratnagiri,
        [Description("Sangli")]
        Sangli,
        [Description("Satara")]
        Satara,
        [Description("Sindhudurg")]
        Sindhudurg,
        [Description("Solapur")]
        Solapur,
        [Description("Thane")]
        Thane,
        [Description("Wardha")]
        Wardha,
        [Description("Washim")]
        Washim,
        [Description("Yavatmal")]
        Yavatmal
    }

    public enum Manipur
    {
        [Description("Bishnupur")]
        Bishnupur,
        [Description("Chandel")]
        Chandel,
        [Description("Churachandpur")]
        Churachandpur,
        [Description("Imphal East")]
        ImphalEast,
        [Description("Imphal West")]
        ImphalWest,
        [Description("Senapati")]
        Senapati,
        [Description("Tamenglong")]
        Tamenglong,
        [Description("Thoubal")]
        Thoubal,
        [Description("Ukhrul")]
        Ukhrul
    }

    public enum Meghalaya
    {
        [Description("East Garo Hills")]
        EastGaroHills,
        [Description("East Khasi Hills")]
        EastKhasiHills,
        [Description("Jaintia Hills")]
        JaintiaHills,
        [Description("RiBhoi")]
        RiBhoi,
        [Description("South Garo Hills")]
        SouthGaroHills,
        [Description("West Garo Hills")]
        WestGaroHills,
        [Description("West Khasi Hills")]
        WestKhasiHills
    }

    public enum Mizoram
    {
        [Description("Aizawl")]
        Aizawl,
        [Description("Champhai")]
        Champhai,
        [Description("Kolasib")]
        Kolasib,
        [Description("Lawngtlai")]
        Lawngtlai,
        [Description("Lunglei")]
        Lunglei,
        [Description("Mamit")]
        Mamit,
        [Description("Saiha")]
        Saiha,
        [Description("Serchhip")]
        Serchhip
    }

    public enum Nagaland
    {
        [Description("Dimapur")]
        Dimapur,
        [Description("Kiphire")]
        Kiphire,
        [Description("Kohima")]
        Kohima,
        [Description("Longleng")]
        Longleng,
        [Description("Mokokchung")]
        Mokokchung,
        [Description("Mon")]
        Mon,
        [Description("Peren")]
        Peren,
        [Description("Phek")]
        Phek,
        [Description("Tuensang")]
        Tuensang,
        [Description("Wokha")]
        Wokha,
        [Description("Zunheboto")]
        Zunheboto
    }

    public enum Delhi
    {
        [Description("Central Delhi")]
        CentralDelhi,
        [Description("East Delhi")]
        EastDelhi,
        [Description("New Delhi")]
        NewDelhi,
        [Description("North Delhi")]
        NorthDelhi,
        [Description("North East Delhi")]
        NorthEastDelhi,
        [Description("North West Delhi")]
        NorthWestDelhi,
        [Description("South Delhi")]
        SouthDelhi,
        [Description("South West Delhi")]
        SouthWestDelhi,
        [Description("West Delhi")]
        WestDelhi
    }

    public enum Orissa
    {
        [Description("Angul")]
        Angul,
        [Description("Balangir")]
        Balangir,
        [Description("Balasore")]
        Balasore,
        [Description("Bargarh (Baragarh)")]
        BargarhBaragarh,
        [Description("Bhadrak")]
        Bhadrak,
        [Description("Boudh Bauda")]
        BoudhBauda,
        [Description("Cuttack")]
        Cuttack,
        [Description("Debagarh (Deogarh)")]
        DebagarhDeogarh,
        [Description("Dhenkanal")]
        Dhenkanal,
        [Description("Gajapati")]
        Gajapati,
        [Description("Ganjam")]
        Ganjam,
        [Description("Jagatsinghpur")]
        Jagatsinghpur,
        [Description("Jajpur")]
        Jajpur,
        [Description("Jharsuguda")]
        Jharsuguda,
        [Description("Kalahandi")]
        Kalahandi,
        [Description("Kandhamal")]
        Kandhamal,
        [Description("Kendrapara")]
        Kendrapara,
        [Description("Kendujhar (Keonjhar)")]
        KendujharKeonjhar,
        [Description("Khordha")]
        Khordha,
        [Description("Koraput")]
        Koraput,
        [Description("Malkangiri")]
        Malkangiri,
        [Description("Mayurbhanj")]
        Mayurbhanj,
        [Description("Nabarangpur")]
        Nabarangpur,
        [Description("Nayagarh")]
        Nayagarh,
        [Description("Nuapada")]
        Nuapada,
        [Description("Puri")]
        Puri,
        [Description("Rayagada")]
        Rayagada,
        [Description("Sambalpur")]
        Sambalpur,
        [Description("Subarnapur (Sonepur)")]
        SubarnapurSonepur,
        [Description("Sundargarh (Sundargarh)")]
        SundargarhSundergarh
    }

    public enum Puducherry
    {
        [Description("Karaikal")]
        Karaikal,
        [Description("Mahe")]
        Mahe,
        [Description("Puducherry")]
        Puducherry,
        [Description("Yanam")]
        Yanam
    }

    public enum Punjab
    {
        [Description("Amritsar")]
        Amritsar,
        [Description("Barnala")]
        Barnala,
        [Description("Bathinda")]
        Bathinda,
        [Description("Firozpur")]
        Firozpur,
        [Description("Faridkot")]
        Faridkot,
        [Description("Fazilka")]
        Fazilka,
        [Description("Fatehgarh Sahib")]
        FatehgarhSahib,
        [Description("Gurdaspur")]
        Gurdaspur,
        [Description("Hoshiarpur")]
        Hoshiarpur,
        [Description("Jalandhar")]
        Jalandhar,
        [Description("Kapurthala")]
        Kapurthala,
        [Description("Ludhiana")]
        Ludhiana,
        [Description("Mansa")]
        Mansa,
        [Description("Moga")]
        Moga,
        [Description("Mukatsar")]
        Mukatsar,
        [Description("Pathankot")]
        Pathankot,
        [Description("Shahid Bhagat Singh Nagar")]
        ShahidBhagatSinghNagar,
        [Description("Patiala")]
        Patiala,
        [Description("Sahibzada Ajit Singh Nagar")]
        SahibzadaAjitSinghNagar,
        [Description("Rupnagar")]
        Rupnagar,
        [Description("Sangrur")]
        Sangrur,
        [Description("Tarn Taran")]
        TarnTaran
    }

    public enum Rajasthan
    {
        [Description("Ajmer")]
        Ajmer,
        [Description("Almer")]
        Alwar,
        [Description("Banswara")]
        Banswara,
        [Description("Baran")]
        Baran,
        [Description("Barmer")]
        Barmer,
        [Description("Bharatpur")]
        Bharatpur,
        [Description("Bhilwara")]
        Bhilwara,
        [Description("Bikaner")]
        Bikaner,
        [Description("Bundi")]
        Bundi,
        [Description("Chittorgarh")]
        Chittorgarh,
        [Description("Churu")]
        Churu,
        [Description("Dausa")]
        Dausa,
        [Description("Dholpur")]
        Dholpur,
        [Description("Dungapur")]
        Dungapur,
        [Description("Ganganagar")]
        Ganganagar,
        [Description("Hanumangarh")]
        Hanumangarh,
        [Description("Jaipur")]
        Jaipur,
        [Description("Jaisalmer")]
        Jaisalmer,
        [Description("Jalore")]
        Jalore,
        [Description("Jhalawar")]
        Jhalawar,
        [Description("Jhunjhunu")]
        Jhunjhunu,
        [Description("Jodhpur")]
        Jodhpur,
        [Description("Karauli")]
        Karauli,
        [Description("Kota")]
        Kota,
        [Description("Nagaur")]
        Nagaur,
        [Description("Pali")]
        Pali,
        [Description("Pratapgarh")]
        Pratapgarh,
        [Description("Rajsamand")]
        Rajsamand,
        [Description("Sawai Madhopur")]
        SawaiMadhopur,
        [Description("Sikar")]
        Sikar,
        [Description("Sirohi")]
        Sirohi,
        [Description("Tonk")]
        Tonk,
        [Description("Udaipur")]
        Udaipur
    }

    public enum Sikkim
    {
        [Description("East Sikkim")]
        EastSikkim,
        [Description("North Sikkim")]
        NorthSikkim,
        [Description("South Sikkim")]
        SouthSikkim,
        [Description("West Sikkim")]
        WestSikkim

    }

    public enum TamilNadu
    {
        [Description("Ariyalur")]
        Ariyalur,
        [Description("Chennai")]
        Chennai,
        [Description("Coimbatore")]
        Coimbatore,
        [Description("Cuddalore")]
        Cuddalore,
        [Description("Dharmapuri")]
        Dharmapuri,
        [Description("Dindigul")]
        Dindigul,
        [Description("Erode")]
        Erode,
        [Description("Kanchipuram")]
        Kanchipuram,
        [Description("Kanyakumari")]
        Kanyakumari,
        [Description("Karur")]
        Karur,
        [Description("Krishnagiri")]
        Krishnagiri,
        [Description("Madurai")]
        Madurai,
        [Description("Nagapattinam")]
        Nagapattinam,
        [Description("Namakkal")]
        Namakkal,
        [Description("Nilgiris")]
        Nilgiris,
        [Description("Perambalur")]
        Perambalur,
        [Description("Pudukkottai")]
        Pudukkottai,
        [Description("Ramanathapuram")]
        Ramanathapuram,
        [Description("Salem")]
        Salem,
        [Description("Sivaganga")]
        Sivaganga,
        [Description("Thanjavur")]
        Thanjavur,
        [Description("Theni")]
        Theni,
        [Description("Thiruvallur")]
        Thiruvallur,
        [Description("Thiruvarur")]
        Thiruvarur,
        [Description("Thoothukudi")]
        Thoothukudi,
        [Description("Tiruchirappalli")]
        Tiruchirappalli,
        [Description("Tirunelveli")]
        Tirunelveli,
        [Description("Tiruppur")]
        Tiruppur,
        [Description("Tiruvannamalai")]
        Tiruvannamalai,
        [Description("Vellore")]
        Vellore,
        [Description("Viluppuram")]
        Viluppuram,
        [Description("Virudhunagar")]
        Virudhunagar
    }

    public enum Tripura
    {
        [Description("Dhalai")]
        Dhalai,
        [Description("North Tripura")]
        NorthTripura,
        [Description("South Tripura")]
        SouthTripura,
        [Description("West Tripura")]
        WestTripura
    }

    public enum UttarPradesh
    {
        [Description("Agra")]
        Agra,
        [Description("Aligarh")]
        Aligarh,
        [Description("Allahabad")]
        Allahabad,
        [Description("Ambedkar Nagar")]
        AmbedkarNagar,
        [Description("Auraiya")]
        Auraiya,
        [Description("Azamgarh")]
        Azamgarh,
        [Description("Bagpat")]
        Bagpat,
        [Description("Bahraich")]
        Bahraich,
        [Description("Ballia")]
        Ballia,
        [Description("Balrampur")]
        Balrampur,
        [Description("Banda")]
        Banda,
        [Description("Barabanki")]
        Barabanki,
        [Description("Bareilly")]
        Bareilly,
        [Description("Basti")]
        Basti,
        [Description("Bijnor")]
        Bijnor,
        [Description("Budaun")]
        Budaun,
        [Description("Bulandshahar")]
        Bulandshahar,
        [Description("Chandauli")]
        Chandauli,
        [Description("Chhatrapati Shahuji Maharaj Nagar")]
        ChhatrapatiShahujiMaharajNagar,
        [Description("Chitrakoot")]
        Chitrakoot,
        [Description("Deoria")]
        Deoria,
        [Description("Etah")]
        Etah,
        [Description("Etawah")]
        Etawah,
        [Description("Faizabad")]
        Faizabad,
        [Description("Farrukhabad")]
        Farrukhabad,
        [Description("Fatehpur")]
        Fatehpur,
        [Description("Firozabad")]
        Firozabad,
        [Description("Gautam Buddha Nagar")]
        GautamBuddhaNagar,
        [Description("Ghaziabad")]
        Ghaziabad,
        [Description("Ghazipur")]
        Ghazipur,
        [Description("Gonda")]
        Gonda,
        [Description("Gorakhpur")]
        Gorakhpur,
        [Description("Hamirpur")]
        Hamirpur,
        [Description("Hardoi")]
        Hardoi,
        [Description("Jalaun")]
        Jalaun,
        [Description("Jaunpur District")]
        JaunpurDistrict,
        [Description("Jhansi")]
        Jhansi,
        [Description("Jyotiba Phule Nagar")]
        JyotibaPhuleNagar,
        [Description("Kannauj")]
        Kannauj,
        [Description("Kanpur Nagar")]
        KanpurNagar,
        [Description("Kanpur South")]
        KanpurSouth,
        [Description("Kanshi Ram Nagar")]
        KanshiRamNagar,
        [Description("Kaushambi")]
        Kaushambi,
        [Description("Kushinagar")]
        Kushinagar,
        [Description("Lakhimpur Kheri")]
        LakhimpurKheri,
        [Description("Lalitpur")]
        Lalitpur,
        [Description("Lucknow")]
        Lucknow,
        [Description("Mahamaya Nagar")]
        MahamayaNagar,
        [Description("Maharajganj")]
        Maharajganj,
        [Description("Mahoba")]
        Mahoba,
        [Description("Mainpuri")]
        Mainpuri,
        [Description("Mathura")]
        Mathura,
        [Description("Mau")]
        Mau,
        [Description("Meerut")]
        Meerut,
        [Description("Mirzapur")]
        Mirzapur,
        [Description("Moradabad")]
        Moradabad,
        [Description("Muzaffarnagar")]
        Muzaffarnagar,
        [Description("Pilibhit")]
        Pilibhit,
        [Description("Pratapgarh")]
        Pratapgarh,
        [Description("Raebareli")]
        Raebareli,
        [Description("RamabaiNagar Kanpur Dehat")]
        RamabaiNagarKanpurDehat,
        [Description("Rampur")]
        Rampur,
        [Description("Saharanpur")]
        Saharanpur,
        [Description("Sant Kabir Nagar")]
        SantKabirNagar,
        [Description("Sant Ravidas Nagar")]
        SantRavidasNagar,
        [Description("Shahjahanpur")]
        Shahjahanpur,
        [Description("Shravasti")]
        Shravasti,
        [Description("Siddharthnagar")]
        Siddharthnagar,
        [Description("Sitapur")]
        Sitapur,
        [Description("Sonbhadra")]
        Sonbhadra,
        [Description("Sultanpur")]
        Sultanpur,
        [Description("Unnao")]
        Unnao,
        [Description("Varanasi")]
        Varanasi
    }

    public enum Uttarakhand
    {
        [Description("Almora")]
        Almora,
        [Description("Bageshwar")]
        Bageshwar,
        [Description("Chamoli")]
        Chamoli,
        [Description("Champawat")]
        Champawat,
        [Description("Dehradun")]
        Dehradun,
        [Description("Haridwar")]
        Haridwar,
        [Description("Nainital")]
        Nainital,
        [Description("Pauri Garhwal")]
        PauriGarhwal,
        [Description("Pithoragarh")]
        Pithoragarh,
        [Description("Rudraprayag")]
        Rudraprayag,
        [Description("Tehri Garhwal")]
        TehriGarhwal,
        [Description("Udham Singh Nagar")]
        UdhamSinghNagar,
        [Description("Uttarkashi")]
        Uttarkashi
    }

    public enum WestBengal
    {
        [Description("Bankura")]
        Bankura,
        [Description("Bardhaman")]
        Bardhaman,
        [Description("Birbhum")]
        Birbhum,
        [Description("Cooch Behar")]
        CoochBehar,
        [Description("Dakshin Dinajpur")]
        DakshinDinajpur,
        [Description("Darjeeling")]
        Darjeeling,
        [Description("Hooghly")]
        Hooghly,
        [Description("Howrah")]
        Howrah,
        [Description("Jalpaiguri")]
        Jalpaiguri,
        [Description("Kolkata")]
        Kolkata,
        [Description("Malda")]
        Malda,
        [Description("Murshidabad")]
        Murshidabad,
        [Description("Nadia")]
        Nadia,
        [Description("North Parganas")]
        NorthParganas,
        [Description("Paschim Medinipur")]
        PaschimMedinipur,
        [Description("Purba Medinipur")]
        PurbaMedinipur,
        [Description("Purulia")]
        Purulia,
        [Description("South Parganas")]
        SouthParganas,
        [Description("Uttar Dinajpur")]
        UttarDinajpur
    }

    #endregion
}
