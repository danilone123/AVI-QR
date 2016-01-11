using System;
using System.Collections.Generic;
using System.Text;

namespace SACommon
{
    public class LanguageUtils
    {
        private static Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();

        static LanguageUtils()
        {
            ENLanguages();
            ESLanguages();
        }

        private static void ENLanguages()
        {
            Dictionary<string, string> enLanguages = new Dictionary<string, string>();

            enLanguages.Add("af", "Afrikaans");
            //enLanguages.Add("af-ZA", "Afrikaans (South Africa)");
            enLanguages.Add("sq", "Albanian");
            //enLanguages.Add("sq-AL", "Albanian (Albania)");
            enLanguages.Add("ar", "Arabic");
            //enLanguages.Add("ar-DZ", "Arabic (Algeria)");
            //enLanguages.Add("ar-BH", "Arabic (Bahrain)");
            //enLanguages.Add("ar-EG", "Arabic (Egypt)");
            //enLanguages.Add("ar-IQ", "Arabic (Iraq)");
            //enLanguages.Add("ar-JO", "Arabic (Jordan)");
            //enLanguages.Add("ar-KW", "Arabic (Kuwait)");
            //enLanguages.Add("ar-LB", "Arabic (Lebanon)");
            //enLanguages.Add("ar-LY", "Arabic (Libya)");
            //enLanguages.Add("ar-MA", "Arabic (Morocco)");
            //enLanguages.Add("ar-OM", "Arabic (Oman)");
            //enLanguages.Add("ar-QA", "Arabic (Qatar)");
            //enLanguages.Add("ar-SA", "Arabic (Saudi Arabia)");
            //enLanguages.Add("ar-SY", "Arabic (Syria)");
            //enLanguages.Add("ar-TN", "Arabic (Tunisia)");
            //enLanguages.Add("ar-AE", "Arabic (U.A.E.)");
            //enLanguages.Add("ar-YE", "Arabic (Yemen)");
            enLanguages.Add("hy", "Armenio");
            //enLanguages.Add("hy-AM", "Armenio (Armenia)");
            enLanguages.Add("az", "Azeri");
            //enLanguages.Add("az-Cyrl-AZ", "Azeri (Azerbaijan, Cyrillic)");
            //enLanguages.Add("az-Latn-AZ", "Azeri (Azerbaijan, Latin)");
            enLanguages.Add("eu", "Basque");
            //enLanguages.Add("eu-ES", "Basque (Basque)");
            enLanguages.Add("be", "Belarusian");
            //enLanguages.Add("be-BY", "Belarusian (Belarus)");
            enLanguages.Add("bg", "Bulgarian");
            //enLanguages.Add("bg-BG", "Bulgarian (Bulgaria)");
            enLanguages.Add("ca", "Catalan");
            //enLanguages.Add("ca-ES", "Catalan (Catalan)");
            //enLanguages.Add("zh-HK", "Chinese (Hong Kong SAR, PRC)");
            //enLanguages.Add("zh-MO", "Chinese (Macao SAR)");
            //enLanguages.Add("zh-CN", "Chinese (PRC)");
            enLanguages.Add("zh-Hans", "Chinese (Simplified)");
            //enLanguages.Add("zh-SG", "Chinese (Singapore)");
            //enLanguages.Add("zh-TW", "Chinese (Taiwan)");
            enLanguages.Add("zh-Hant", "Chinese (Traditional)");
            enLanguages.Add("hr", "Croatian");
            //enLanguages.Add("hr-HR", "Croatian (Croatia)");
            enLanguages.Add("cs", "Czech");
            //enLanguages.Add("cs-CZ", "Czech (Czech Republic)");
            enLanguages.Add("da", "Danish");
            //enLanguages.Add("da-DK", "Danish (Denmark)");
            enLanguages.Add("dv", "Divehi");
            //enLanguages.Add("dv-MV", "Divehi (Maldives)");
            enLanguages.Add("nl", "Dutch");
            //enLanguages.Add("nl-BE", "Dutch (Belgium)");
            //enLanguages.Add("nl-NL", "Dutch (Netherlands)");
            enLanguages.Add("en", "English");
            //enLanguages.Add("en-AU", "English (Australia)");
            //enLanguages.Add("en-BZ", "English (Belize)");
            //enLanguages.Add("en-CA", "English (Canada)");
            //enLanguages.Add("en-029", "English (Caribbean)");
            //enLanguages.Add("en-IE", "English (Ireland)");
            //enLanguages.Add("en-JM", "English (Jamaica)");
            //enLanguages.Add("en-NZ", "English (New Zealand)");
            //enLanguages.Add("en-PH", "English (Philippines)");
            //enLanguages.Add("en-ZA", "English (South Africa");
            //enLanguages.Add("en-TT", "English (Trinidad and Tobago)");
            //enLanguages.Add("en-GB", "English (United Kingdom)");
            //enLanguages.Add("en-US", "English (United States)");
            //enLanguages.Add("en-ZW", "English (Zimbabwe)");
            enLanguages.Add("et", "Estonian");
            //enLanguages.Add("et-EE", "Estonian (Estonia)");
            enLanguages.Add("fo", "Faroese");
            //enLanguages.Add("fo-FO", "Faroese (Faroe Islands)");
            enLanguages.Add("fa", "Farsi");
            //enLanguages.Add("fa-IR", "Farsi (Iran)");
            enLanguages.Add("fi", "Finnish");
            //enLanguages.Add("fi-FI", "Finnish (Finland)");
            enLanguages.Add("fr", "French");
            //enLanguages.Add("fr-BE", "French (Belgium)");
            //enLanguages.Add("fr-CA", "French (Canada)");
            //enLanguages.Add("fr-FR", "French (France)");
            //enLanguages.Add("fr-LU", "French (Luxembourg)");
            //enLanguages.Add("fr-MC", "French (Monaco)");
            //enLanguages.Add("fr-CH", "French (Switzerland)");
            enLanguages.Add("gl", "Galician");
            //enLanguages.Add("gl-ES", "Galician (Spain)");
            enLanguages.Add("ka", "Georgian");
            //enLanguages.Add("ka-GE", "Georgian (Georgia)");
            enLanguages.Add("de", "German");
            //enLanguages.Add("de-AT", "German (Austria)");
            //enLanguages.Add("de-DE", "German (Germany)");
            //enLanguages.Add("de-LI", "German (Liechtenstein)");
            //enLanguages.Add("de-LU", "German (Luxembourg)");
            //enLanguages.Add("de-CH", "German (Switzerland)");
            enLanguages.Add("el", "Greek");
            //enLanguages.Add("el-GR", "Greek (Greece)");
            enLanguages.Add("gu", "Gujarati");
            //enLanguages.Add("gu-IN", "Gujarati (India)");
            enLanguages.Add("he", "Hebrew");
            //enLanguages.Add("he-IL", "Hebrew (Israel)");
            enLanguages.Add("hi", "Hindi");
            //enLanguages.Add("hi-IN", "Hindi (India)");
            enLanguages.Add("hu", "Hungarian");
            //enLanguages.Add("hu-HU", "Hungarian (Hungary)");
            enLanguages.Add("is", "Icelandic");
            //enLanguages.Add("is-IS", "Icelandic (Iceland)");
            enLanguages.Add("id", "Indonesian");
            //enLanguages.Add("id-ID", "Indonesian (Indonesia)");
            enLanguages.Add("it", "Italian");
            //enLanguages.Add("it-IT", "Italian (Italy)");
            //enLanguages.Add("it-CH", "Italian (Switzerland)");
            enLanguages.Add("ja", "Japanese");
            //enLanguages.Add("ja-JP", "Japanese (Japan)");
            enLanguages.Add("kn", "Kannada");
            //enLanguages.Add("kn-IN", "Kannada (India)");
            enLanguages.Add("kk", "Kazakh");
            //enLanguages.Add("kk-KZ", "Kazakh (Kazakhstan)");
            enLanguages.Add("kok", "Konkani");
            //enLanguages.Add("kok-IN", "Konkani (India)");
            enLanguages.Add("ko", "Korean");
            //enLanguages.Add("ko-KR", "Korean (Korea)");
            enLanguages.Add("ky", "Kyrgyz");
            //enLanguages.Add("ky-KG", "Kyrgyz (Kyrgyzstan)");
            enLanguages.Add("lv", "Latvian");
            //enLanguages.Add("lv-LV", "Latvian (Latvia)");
            enLanguages.Add("lt", "Lithuanian");
            //enLanguages.Add("lt-LT", "Lithuanian (Lithuania)");
            enLanguages.Add("mk", "Macedonian");
            //enLanguages.Add("mk-MK", "Macedonian (Macedonia, FYROM)");
            enLanguages.Add("ms", "Malay");
            //enLanguages.Add("ms-BN", "Malay (Brunei Darussalam)");
            //enLanguages.Add("ms-MY", "Malay (Malaysia)");
            enLanguages.Add("mr", "Marathi");
            //enLanguages.Add("mr-IN", "Marathi (India)");
            enLanguages.Add("mn", "Mongolian");
            //enLanguages.Add("mn-MN", "Mongolian (Mongolia)");
            enLanguages.Add("no", "Norwegian");
            //enLanguages.Add("nb-NO", "Norwegian (Bokmål, Norway)");
            //enLanguages.Add("nn-NO", "Norwegian (Nynorsk, Norway)");
            enLanguages.Add("pl", "Polish");
            //enLanguages.Add("pl-PL", "Polish (Poland)");
            enLanguages.Add("pt", "Portuguese");
            //enLanguages.Add("pt-BR", "Portuguese (Brazil)");
            //enLanguages.Add("pt-PT", "Portuguese (Portugal)");
            enLanguages.Add("pa", "Punjabi");
            //enLanguages.Add("pa-IN", "Punjabi (India)");
            enLanguages.Add("ro", "Romanian");
            //enLanguages.Add("ro-RO", "Romanian (Romania)");
            enLanguages.Add("ru", "Russian");
            //enLanguages.Add("ru-RU", "Russian (Russia)");
            enLanguages.Add("sa", "Sanskrit");
            //enLanguages.Add("sa-IN", "Sanskrit (India)");
            enLanguages.Add("sr", "Serbian");
            //enLanguages.Add("sr-Cyrl-CS", "Serbian (Serbia, Cyrillic)");
            //enLanguages.Add("sr-Latn-CS", "Serbian (Serbia, Latin)");
            enLanguages.Add("sk", "Slovak");
            //enLanguages.Add("sk-SK", "Slovak (Slovakia)");
            enLanguages.Add("sl", "Slovenian");
            //enLanguages.Add("sl-SI", "Slovenian (Slovenia)");
            enLanguages.Add("es", "Spanish");
            //enLanguages.Add("es-AR", "Spanish (Argentina)");
            //enLanguages.Add("es-BO", "Spanish (Bolivia)");
            //enLanguages.Add("es-CL", "Spanish (Chile)");
            //enLanguages.Add("es-CO", "Spanish (Colombia)");
            //enLanguages.Add("es-CR", "Spanish (Costa Rica)");
            //enLanguages.Add("es-DO", "Spanish (Dominican Republic)");
            //enLanguages.Add("es-EC", "Spanish (Ecuador)");
            //enLanguages.Add("es-SV", "Spanish (El Salvador)");
            //enLanguages.Add("es-GT", "Spanish (Guatemala)");
            //enLanguages.Add("es-HN", "Spanish (Honduras)");
            //enLanguages.Add("es-MX", "Spanish (Mexico)");
            //enLanguages.Add("es-NI", "Spanish (Nicaragua)");
            //enLanguages.Add("es-PA", "Spanish (Panama)");
            //enLanguages.Add("es-PY", "Spanish (Paraguay)");
            //enLanguages.Add("es-PE", "Spanish (Peru)");
            //enLanguages.Add("es-PR", "Spanish (Puerto Rico)");
            //enLanguages.Add("es-ES", "Spanish (Spain)");
            //enLanguages.Add("es-ES_tradnl", "Spanish (Spain, Traditional Sort)");
            //enLanguages.Add("es-UY", "Spanish (Uruguay)");
            //enLanguages.Add("es-VE", "Spanish (Venezuela)");
            enLanguages.Add("sw", "Swahili");
            //enLanguages.Add("sw-KE", "Swahili (Kenya)");
            enLanguages.Add("sv", "Swedish");
            //enLanguages.Add("sv-FI", "Swedish (Finland)");
            //enLanguages.Add("sv-SE", "Swedish (Sweden)");
            enLanguages.Add("syr", "Syriac");
            //enLanguages.Add("syr-SY", "Syriac (Syria)");
            enLanguages.Add("ta", "Tamil");
            //enLanguages.Add("ta-IN", "Tamil (India)");
            enLanguages.Add("tt", "Tatar");
            //enLanguages.Add("tt-RU", "Tatar (Russia)");
            enLanguages.Add("te", "Telugu");
            //enLanguages.Add("te-IN", "Telugu (India)");
            enLanguages.Add("th", "Thai");
            //enLanguages.Add("th-TH", "Thai (Thailand)");
            enLanguages.Add("tr", "Turkish");
            //enLanguages.Add("tr-TR", "Turkish (Turkey)");
            enLanguages.Add("uk", "Ukrainian");
            //enLanguages.Add("uk-UA", "Ukrainian (Ukraine)");
            enLanguages.Add("ur", "Urdu");
            //enLanguages.Add("ur-PK", "Urdu (Pakistan)");
            enLanguages.Add("uz", "Uzbek");
            //enLanguages.Add("uz-Cyrl-UZ", "Uzbek (Uzbekistan, Cyrillic)");
            //enLanguages.Add("uz-Latn-UZ", "Uzbek (Uzbekistan, Latin)");
            enLanguages.Add("vi", "Vietnamese");
            //enLanguages.Add("vi-VN", "Vietnamese (Vietnam)");

            languages.Add("en", enLanguages);
        }

        private static void ESLanguages()
        {
            Dictionary<string, string> esLanguages = new Dictionary<string, string>();

            esLanguages.Add("af", "Afrikaans");
            //esLanguages.Add("af-ZA", "Afrikaans (Sudáfrica)");
            esLanguages.Add("sq", "Albanés");
            //esLanguages.Add("sq-AL", "Albanés (Albania)");
            esLanguages.Add("de", "Alemán");
            //esLanguages.Add("de-AT", "Alemán (Austria)");
            //esLanguages.Add("de-DE", "Alemán (Alemania)");
            //esLanguages.Add("de-LI", "Alemán (Liechtenstein)");
            //esLanguages.Add("de-LU", "Alemán (Luxemburgo)");
            //esLanguages.Add("de-CH", "Alemán (Suiza)");
            esLanguages.Add("ar", "Árabe");
            //esLanguages.Add("ar-DZ", "Árabe (Argelia)");
            //esLanguages.Add("ar-BH", "Árabe (Bahrein)");
            //esLanguages.Add("ar-EG", "Árabe (Egipto)");
            //esLanguages.Add("ar-IQ", "Árabe (Iraq)");
            //esLanguages.Add("ar-JO", "Árabe (Jordania)");
            //esLanguages.Add("ar-KW", "Árabe (Kuwait)");
            //esLanguages.Add("ar-LB", "Árabe (Líbano)");
            //esLanguages.Add("ar-LY", "Árabe (Libia)");
            //esLanguages.Add("ar-MA", "Árabe (Marruecos)");
            //esLanguages.Add("ar-OM", "Árabe (Omán)");
            //esLanguages.Add("ar-QA", "Árabe (Qatar)");
            //esLanguages.Add("ar-SA", "Árabe (Arabia Saudita)");
            //esLanguages.Add("ar-SY", "Árabe (Siria)");
            //esLanguages.Add("ar-TN", "Árabe (Túnez)");
            //esLanguages.Add("ar-AE", "Árabe (U.A.E.)");
            //esLanguages.Add("ar-YE", "Árabe (Yemen)");
            esLanguages.Add("hy", "Armenio");
            //esLanguages.Add("hy-AM", "Armenio (Armenia)");
            esLanguages.Add("az", "Azerí");
            //esLanguages.Add("az-Cyrl-AZ", "Azerí (Azerbaiyán, Cirílico)");
            //esLanguages.Add("az-Latn-AZ", "Azerí (Azerbaiyán, Latín)");
            esLanguages.Add("be", "Bielorruso");
            //esLanguages.Add("be-BY", "Bielorruso (Bielorrusia)");
            esLanguages.Add("bg", "Búlgaro");
            //esLanguages.Add("bg-BG", "Búlgaro (Bulgaria)");
            esLanguages.Add("kn", "Canarés");
            //esLanguages.Add("kn-IN", "Canarés (India)");
            esLanguages.Add("ca", "Catalán");
            //esLanguages.Add("ca-ES", "Catalán (Cataluña)");
            esLanguages.Add("cs", "Checo");
            //esLanguages.Add("cs-CZ", "Checo (República Checa)");
            //esLanguages.Add("zh-HK", "Chino (Hong Kong SAR, PRC)");
            //esLanguages.Add("zh-MO", "Chino (Macao SAR)");
            //esLanguages.Add("zh-CN", "Chino (PRC)");
            esLanguages.Add("zh-Hans", "Chino (Simplificado)");
            //esLanguages.Add("zh-SG", "Chino (Singapur)");
            //esLanguages.Add("zh-TW", "Chino (Taiwán)");
            esLanguages.Add("zh-Hant", "Chino (Tradicional)");
            esLanguages.Add("ko", "Coreano");
            //esLanguages.Add("ko-KR", "Coreano (Corea)");
            esLanguages.Add("hr", "Croata");
            //esLanguages.Add("hr-HR", "Croata (Croacia)");
            esLanguages.Add("da", "Danés");
            //esLanguages.Add("da-DK", "Danés (Dinamarca)");
            esLanguages.Add("dv", "Dhivehi");
            //esLanguages.Add("dv-MV", "Dhivehi (Islas Maldivas)");
            esLanguages.Add("sk", "Eslovaco");
            //esLanguages.Add("sk-SK", "Eslovaco (Eslovaquia)");
            esLanguages.Add("sl", "Esloveno");
            //esLanguages.Add("sl-SI", "Esloveno (Eslovenia)");
            esLanguages.Add("es", "Español");
            //esLanguages.Add("es-AR", "Español (Argentina)");
            //esLanguages.Add("es-BO", "Español (Bolivia)");
            //esLanguages.Add("es-CL", "Español (Chile)");
            //esLanguages.Add("es-CO", "Español (Colombia)");
            //esLanguages.Add("es-CR", "Español (Costa Rica)");
            //esLanguages.Add("es-DO", "Español (República Dominicana)");
            //esLanguages.Add("es-EC", "Español (Ecuador)");
            //esLanguages.Add("es-SV", "Español (El Salvador)");
            //esLanguages.Add("es-GT", "Español (Guatemala)");
            //esLanguages.Add("es-HN", "Español (Honduras)");
            //esLanguages.Add("es-MX", "Español (México)");
            //esLanguages.Add("es-NI", "Español (Nicaragua)");
            //esLanguages.Add("es-PA", "Español (Panamá)");
            //esLanguages.Add("es-PY", "Español (Paraguay)");
            //esLanguages.Add("es-PE", "Español (Perú)");
            //esLanguages.Add("es-PR", "Español (Puerto Rico)");
            //esLanguages.Add("es-ES", "Español (España)");
            //esLanguages.Add("es-ES_tradnl", "Español (España, Orden Tradicional)");
            //esLanguages.Add("es-UY", "Español (Uruguay)");
            //esLanguages.Add("es-VE", "Español (Venezuela)");
            esLanguages.Add("et", "Estonio");
            //esLanguages.Add("et-EE", "Estonio (Estonia)");
            esLanguages.Add("fo", "Feroés");
            //esLanguages.Add("fo-FO", "Feroés (Islas Feroe)");
            esLanguages.Add("fi", "Finés");
            //esLanguages.Add("fi-FI", "Finés (Finlandia)");
            esLanguages.Add("fr", "Francés");
            //esLanguages.Add("fr-BE", "Francés (Bélgica)");
            //esLanguages.Add("fr-CA", "Francés (Canadá)");
            //esLanguages.Add("fr-FR", "Francés (Francia)");
            //esLanguages.Add("fr-LU", "Francés (Luxemburgo)");
            //esLanguages.Add("fr-MC", "Francés (Mónaco)");
            //esLanguages.Add("fr-CH", "Francés (Suiza)");
            esLanguages.Add("gl", "Gallego");
            //esLanguages.Add("gl-ES", "Gallego (España)");
            esLanguages.Add("ka", "Georgiano");
            //esLanguages.Add("ka-GE", "Georgiano (Georgia)");
            esLanguages.Add("el", "Griego");
            //esLanguages.Add("el-GR", "Griego (Grecia)");
            esLanguages.Add("gu", "Gujarati");
            //esLanguages.Add("gu-IN", "Gujarati (India)");
            esLanguages.Add("he", "Hebreo");
            //esLanguages.Add("he-IL", "Hebreo (Israel)");
            esLanguages.Add("hi", "Hindi");
            //esLanguages.Add("hi-IN", "Hindi (India)");
            esLanguages.Add("nl", "Holandés");
            //esLanguages.Add("nl-BE", "Holandés (Bélgica)");
            //esLanguages.Add("nl-NL", "Holandés (Países Bajos)");
            esLanguages.Add("hu", "Húngaro");
            //esLanguages.Add("hu-HU", "Húngaro (Hungría)");
            esLanguages.Add("id", "Indonesio");
            //esLanguages.Add("id-ID", "Indonesio (Indonesia)");
            esLanguages.Add("en", "Inglés");
            //esLanguages.Add("en-AU", "Inglés (Australia)");
            //esLanguages.Add("en-BZ", "Inglés (Belice)");
            //esLanguages.Add("en-CA", "Inglés (Canadá)");
            //esLanguages.Add("en-029", "Inglés (Caribe)");
            //esLanguages.Add("en-IE", "Inglés (Irlanda)");
            //esLanguages.Add("en-JM", "Inglés (Jamaica)");
            //esLanguages.Add("en-NZ", "Inglés (Nueva Zelanda)");
            //esLanguages.Add("en-PH", "Inglés (Filipinas)");
            //esLanguages.Add("en-ZA", "Inglés (Sudáfrica");
            //esLanguages.Add("en-TT", "Inglés (Trinidad y Tobago)");
            //esLanguages.Add("en-GB", "Inglés (Reino Unido)");
            //esLanguages.Add("en-US", "Inglés (Estados Unidos)");
            //esLanguages.Add("en-ZW", "Inglés (Zimbabwe)");
            esLanguages.Add("is", "Islandés");
            //esLanguages.Add("is-IS", "Islandés  (Islandia)");
            esLanguages.Add("it", "Italiano");
            //esLanguages.Add("it-IT", "Italiano (Italia)");
            //esLanguages.Add("it-CH", "Italiano (Suiza)");
            esLanguages.Add("ja", "Japonés");
            //esLanguages.Add("ja-JP", "Japonés (Japón)");
            esLanguages.Add("kk", "Kazajo");
            //esLanguages.Add("kk-KZ", "Kazajo (Kazajstán)");
            esLanguages.Add("ky", "Kirguís");
            //esLanguages.Add("ky-KG", "Kirguís (Kirguistán)");
            esLanguages.Add("kok", "Konkani");
            //esLanguages.Add("kok-IN", "Konkani (India)");
            esLanguages.Add("lv", "Letón");
            //esLanguages.Add("lv-LV", "Letón (Letonia)");
            esLanguages.Add("lt", "Lituano");
            //esLanguages.Add("lt-LT", "Lituano (Lituania)");
            esLanguages.Add("mk", "Macedonio");
            //esLanguages.Add("mk-MK", "Macedonio (Macedonia, FYROM)");
            esLanguages.Add("ms", "Malayo");
            //esLanguages.Add("ms-BN", "Malayo (Brunei Darussalam)");
            //esLanguages.Add("ms-MY", "Malayo (Malasia)");
            esLanguages.Add("mr", "Marathi");
            //esLanguages.Add("mr-IN", "Marathi (India)");
            esLanguages.Add("mn", "Mongol");
            //esLanguages.Add("mn-MN", "Mongol (Mongolia)");
            esLanguages.Add("no", "Noruego");
            //esLanguages.Add("nb-NO", "Noruego (Bokmal, Noruega)");
            //esLanguages.Add("nn-NO", "Noruego (Nynorsk, Noruega)");
            esLanguages.Add("fa", "Persa");
            //esLanguages.Add("fa-IR", "Persa (Irán)");
            esLanguages.Add("pl", "Polaco");
            //esLanguages.Add("pl-PL", "Polaco (Polonia)");
            esLanguages.Add("pt", "Portugués");
            //esLanguages.Add("pt-BR", "Portugués (Brasil)");
            //esLanguages.Add("pt-PT", "Portugués (Portugal)");
            esLanguages.Add("pa", "Punjabí");
            //esLanguages.Add("pa-IN", "Punjabí (India)");
            esLanguages.Add("ro", "Rumano");
            //esLanguages.Add("ro-RO", "Rumano (Rumania)");
            esLanguages.Add("ru", "Ruso");
            //esLanguages.Add("ru-RU", "Ruso (Rusia)");
            esLanguages.Add("sa", "Sánscrito");
            //esLanguages.Add("sa-IN", "Sánscrito (India)");
            esLanguages.Add("sr", "Serbio");
            //esLanguages.Add("sr-Cyrl-CS", "Serbio (Serbia, Cirílico)");
            //esLanguages.Add("sr-Latn-CS", "Serbio (Serbia, Latín)");
            esLanguages.Add("syr", "Sirio");
            //esLanguages.Add("syr-SY", "Sirio (Siria)");
            esLanguages.Add("sv", "Sueco");
            //esLanguages.Add("sv-FI", "Sueco (Finlandia)");
            //esLanguages.Add("sv-SE", "Sueco (Suecia)");
            esLanguages.Add("sw", "Swahili");
            //esLanguages.Add("sw-KE", "Swahili (Kenia)");
            esLanguages.Add("th", "Tailandés");
            //esLanguages.Add("th-TH", "Tailandés (Tailandia)");
            esLanguages.Add("ta", "Tamil");
            //esLanguages.Add("ta-IN", "Tamil (India)");
            esLanguages.Add("tt", "Tártaro");
            //esLanguages.Add("tt-RU", "Tártaro (Rusia)");
            esLanguages.Add("te", "Telugu");
            //esLanguages.Add("te-IN", "Telugu (India)");
            esLanguages.Add("tr", "Turco");
            //esLanguages.Add("tr-TR", "Turco (Turquía)");
            esLanguages.Add("uk", "Ucraniano");
            //esLanguages.Add("uk-UA", "Ucraniano (Ucrania)");
            esLanguages.Add("ur", "Urdu");
            //esLanguages.Add("ur-PK", "Urdu (Pakistán)");
            esLanguages.Add("uz", "Uzbeko");
            //esLanguages.Add("uz-Cyrl-UZ", "Uzbeko (Uzbekistán, Cirílico)");
            //esLanguages.Add("uz-Latn-UZ", "Uzbeko (Uzbekistán, Latín)");
            esLanguages.Add("eu", "Vasco");
            //esLanguages.Add("eu-ES", "Vasco (País Vasco)");
            esLanguages.Add("vi", "Vietnamita");
            //esLanguages.Add("vi-VN", "Vietnamita (Vietnam)");

            languages.Add("es", esLanguages);
        }

        //Returns languages dictionary list in the specified site language
        public static Dictionary<string, string> ObtainLanguages(string siteLanguage)
        {
            try
            {
                return languages[siteLanguage.ToLower()];
            }
            catch
            {
                return languages["en"];
            }
        }

        //Returns language name for the specified language code and site language
        public static string ObtainLanguageName(string siteLanguage, string languageCode)
        {
            try
            {
                Dictionary<string, string> siteLanguages = languages[siteLanguage.ToLower()];
                return siteLanguages[languageCode];
            }
            catch
            {
                return (languages["en"])[languageCode];
            }
        }

    }
}
