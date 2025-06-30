import i18next from "i18next";
import {initReactI18next} from "react-i18next";

import UA from "./languages/ua/translation.json";
import EN from "./languages/en/translation.json";

const resources = {
    ua: { translation: UA },
    en: { translation: EN }
};

i18next.use(initReactI18next).init({
    resources,
    lng: 'ua',
    fallbackLng: 'en',
    interpolation: {
        escapeValue: false
    }
});

export default i18next;