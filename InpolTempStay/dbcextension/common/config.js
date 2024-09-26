var Config = {

    default: {
        isPluginEnabled: true,
        userName: "jankustosz1",
        passWord: "Kot12321",
        valute: "USD",
        autoSubmitForms: false,
        submitFormsDelay: 0,
        enabledForNormal: true,
        enabledForRecaptchaV2: true,
        enabledForInvisibleRecaptchaV2: true,
        enabledForRecaptchaV3: true,
        enabledForHCaptcha: true,
        enabledForGeetest: true,
        enabledForGeetest_v4: true,
        enabledForArkoselabs: true,
        autoSolveNormal: true,
        autoSolveRecaptchaV2: true,
        autoSolveInvisibleRecaptchaV2: true,
        autoSolveRecaptchaV3: true,
        recaptchaV3MinScore: 0.5,
        autoSolveHCaptcha: true,
        autoSolveGeetest: true,
        autoSolveArkoselabs: true,
        autoSolveGeetest_v4: true,
        repeatOnErrorTimes: 5,
        repeatOnErrorDelay: 0,
        buttonPosition: 'inner',
        useProxy: false,
        proxytype: "HTTP",
        proxy: "",
        blackListDomain: "example.com\ndeathbycaptcha.com/login\n",
        normalSources: [],
        autoSubmitRules: [{
            url_pattern: "deathbycaptcha.com/login",
            code: "",
        }],
    },

    get: async function (key) {
        let config = await this.getAll();
        return config[key];
    },

    getAll: function () {
        return new Promise(function (resolve, reject) {
            chrome.storage.local.get('config', function (result) {
                resolve(Config.joinObjects(Config.default, result.config));
            });
        });
    },

    set: function (newData) {
        return new Promise(function (resolve, reject) {
            Config.getAll()
                .then(data => {
                    chrome.storage.local.set({
                        config: Config.joinObjects(data, newData)
                    }, function (config) {
                        resolve(config);
                    });
                });
        });
    },

    joinObjects: function (obj1, obj2) {
        let res = {};
        for (let key in obj1) res[key] = obj1[key];
        for (let key in obj2) res[key] = obj2[key];
        return res;
    },

};
