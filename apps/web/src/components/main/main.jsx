import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

function Main() {
    const { t, i18n } = useTranslation();
    const [currentLanguage, setCurrentLanguage] = useState(i18n.language);

    function changeLang(lang) {
        i18n.changeLanguage(lang);
        setCurrentLanguage(lang);
    }

    return (
        <div>
            <nav>
                <Link to="/login">{t("login")}</Link> |{" "}
                <Link to="/register">{t("register")}</Link> |{" "}
                <Link to="/mainMovies">Main Movie</Link> |{" "}
                <Link to="/changePassword">Change Password</Link>
            </nav>

            <button onClick={() => changeLang(currentLanguage === "ua" ? "en" : "ua")}>
                {currentLanguage === "ua" ? "English" : "Українська"}
            </button>
        </div>
    );
}

export default Main;
