using UnityEngine;
using System.Collections.Generic;

public static class LocaleHelper {
    public static Dictionary<string, Dictionary<string, string>> Texts = new Dictionary<string, Dictionary<string, string>>{
        {"EN", new Dictionary<string, string>{
            {"play", "PLAY"},
            {"select_level", "Select Level"},
            {"select_level_upper", "SELECT LEVEL"},
            {"retry", "RETRY"},
            {"connection_message", "Error! \nCheck internet connection!"},
            {"tap_to_continue", "Tap to continue"},
            {"tutorial0", "Change Camera Angle"},
            {"tutorial1", "PICK ORANGE COLOR TO OPEN ORANGE CUBES"},
            {"tutorial2", "MEMORY, you can see colors which you pick before."},
            {"tutorial2_a", "When you pick a color, it's added to the memory. Then your color changes and cubes whose color is yours are opened."},
            {"tutorial3", "RELEASE COLOR"},
            {"tutorial4", "As you see, your color is WHITE and WHITE CUBES opened."},
            {"level1", "LEVEL 1"},
            {"level2", "LEVEL 2"},
            {"level3", "LEVEL 3"},
            {"level4", "LEVEL 4"},
            {"level5", "LEVEL 5"},
            {"level6", "LEVEL 6"},
            {"level7", "LEVEL 7"},
            {"level8", "LEVEL 8"},
            {"level9", "LEVEL 9"},
            {"level10", "LEVEL 10"},
            {"level11", "LEVEL 11"},
            {"level12", "LEVEL 12"},
            {"level13", "LEVEL 13"},
            {"level14", "LEVEL 14"},
            {"level15", "LEVEL 15"},
            {"level16", "LEVEL 16"},
            {"level17", "LEVEL 17"},
            {"level18", "LEVEL 18"},
            {"level19", "LEVEL 19"},
            {"level20", "LEVEL 20"},
            {"reward_message", "Color Added to the memory!"},
            {"take_color", "TAKE COLOR"},
            {"menu", "MENU"},
            {"congrats", "Congragulations!\nYou've finished all levels!\nWe're making a new levels for gamers like you!\nFOLLOW US!"},
            {"reward_helper", "Which color do you need?"},
            {"reward_tutorial", "If you need an extra color, you can take it from here"},
        }},
        {"TR", new Dictionary<string, string>{
            {"play", "OYNA"},
            {"select_level", "Bölüm Seç"},
            {"select_level_upper", "BÖLÜM SEÇ"},
            {"retry", "DENE"},
            {"connection_message", "Hata! \nİnternet bağlantınızı kontrol edin!"},
            {"tap_to_continue", "Devam etmek için dokunun"},
            {"tutorial0", "Kamera Açısını Değiştirin"},
            {"tutorial1", "TURUNCU KÜPLERİ AÇMAK İÇİN TURUNCU RENGİ ALIN"},
            {"tutorial2", "HAFIZA, aldığınız renkleri görebilirsiniz."},
            {"tutorial2_a", "Bir renk alındığında hafızaya eklenir ve o renge bürünürsünüz. Hangi renkteyseniz o renkteki küpler açılır diğerleri kapanır."},
            {"tutorial3", "RENGİ BIRAKIN"},
            {"tutorial4", "Görüldüğü üzere renginiz beyaz olduğu için beyaz küpler açıldı."},
            {"level1", "BÖLÜM 1"},
            {"level2", "BÖLÜM 2"},
            {"level3", "BÖLÜM 3"},
            {"level4", "BÖLÜM 4"},
            {"level5", "BÖLÜM 5"},
            {"level6", "BÖLÜM 6"},
            {"level7", "BÖLÜM 7"},
            {"level8", "BÖLÜM 8"},
            {"level9", "BÖLÜM 9"},
            {"level10", "BÖLÜM 10"},
            {"level11", "BÖLÜM 11"},
            {"level12", "BÖLÜM 12"},
            {"level13", "BÖLÜM 13"},
            {"level14", "BÖLÜM 14"},
            {"level15", "BÖLÜM 15"},
            {"level16", "BÖLÜM 16"},
            {"level17", "BÖLÜM 17"},
            {"level18", "BÖLÜM 18"},
            {"level19", "BÖLÜM 19"},
            {"level20", "BÖLÜM 20"},
            {"reward_message", "Renk Hafızaya eklendi!"},
            {"take_color", "RENK AL"},
            {"menu", "MENU"},
            {"congrats", "Tebrikler!\nBütün bölümleri geçtiniz!\nSizin gibi oyuncular için yeni bölümler yapıyoruz.\nTAKİPTE KALIN!"},
            {"reward_helper", "Hangi renge ihtiyacınız var?"},
            {"reward_tutorial", "Bir renge daha ihtiyacınız olursa, buradan alabilirsiniz."},
        }}
    };

    public static string GetLang() {
        SystemLanguage lang = Application.systemLanguage;

        switch (lang) {
            case SystemLanguage.Turkish:
                return "TR";
            default:
                return GetDefaultSupportedLanguageCode();
        }
    }

    public static string GetDefaultSupportedLanguageCode() {
        return "EN";
    }

    public static string GetText(string key) {
        return Texts[GetLang()][key];
    }
}