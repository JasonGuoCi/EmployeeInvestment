
"use strict"



jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        options = options || {};
        if (value === null) {
            value = '';
            options = $.extend({}, options); // clone object since it's unexpected behavior if the expired property were changed
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        // NOTE Needed to parenthesize options.path and options.domain
        // in the following expressions, otherwise they evaluate to undefined
        // in the packed version for some reason...
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

var SP2013 = window.SP2013 || {};
(function () {
    var DONT_ENUM = "propertyIsEnumerable,isPrototypeOf,hasOwnProperty,toLocaleString,toString,valueOf,constructor".split(","),
    hasOwn = ({}).hasOwnProperty;
    for (var i in {
        toString: 1
    }) {
        DONT_ENUM = false;
    }

    Object.keys = Object.keys || function (obj) {//ecma262v5 15.2.3.14
        var result = [];
        for (var key in obj) if (hasOwn.call(obj, key)) {
            result.push(key)
        }
        if (DONT_ENUM && obj) {
            for (var i = 0 ; key = DONT_ENUM[i++];) {
                if (hasOwn.call(obj, key)) {
                    result.push(key);
                }
            }
        }
        return result;
    };

    if (Object.defineProperty && Object.defineProperties) {

    } else {
        //dose not support get set
        Object.defineProperty = function (target, property, member) {
            if (member.value !== undefined) {
                target[property] = member.value;
                return;
            }
            target[property] = member;
        }

        Object.defineProperties = function (target, members) {
            for (var key in members) {
                Object.defineProperty(target, key, members[key]);
            }
        }
    }

})();
(function (global) {

    //将members属性或者方法加入到target对象中
    function initializeProperties(target, members) {
        var keys = Object.keys(members);
        var properties;
        var i, len;
        for (i = 0, len = keys.length; i < len; i++) {
            var key = keys[i];
            var enumerable = key.charCodeAt(0) !== /*_*/ 95;
            var member = members[key];
            if (member && typeof member === 'object') {
                if (member.value !== undefined || typeof member.get === 'function' || typeof member.set === 'function') {
                    if (member.enumerable === undefined) {
                        member.enumerable = enumerable;
                    }
                    properties = properties || {};
                    properties[key] = member;
                    continue;
                }
            }
            if (!enumerable) {
                properties = properties || {};
                properties[key] = {
                    value: member,
                    enumerable: enumerable,
                    configurable: true,
                    writable: true
                }
                continue;
            }
            target[key] = member;
        }
        if (properties) {
            Object.defineProperties(target, properties);
        }
    }

    //实现js namespace的方法，传入参数为根命名空间
    (function (rootNamespace) {

        // Create the rootNamespace in the global namespace
        if (!global[rootNamespace]) {
            global[rootNamespace] = Object.create(Object.prototype);
        }

        // Cache the rootNamespace we just created in a local variable
        var _rootNamespace = global[rootNamespace];
        //if (!_rootNamespace.Namespace) {
        //    _rootNamespace.Namespace = Object.create(Object.prototype);
        //}

        function defineWithParent(parentNamespace, name, members) {
            var currentNamespace = parentNamespace,
                namespaceFragments = name.split(".");

            for (var i = 0, len = namespaceFragments.length; i < len; i++) {
                var namespaceName = namespaceFragments[i];
                if (!currentNamespace[namespaceName]) {
                    Object.defineProperty(currentNamespace, namespaceName, {
                        value: {},
                        writable: false,
                        enumerable: true,
                        configurable: true
                    });
                }
                currentNamespace = currentNamespace[namespaceName];
            }

            if (members) {
                initializeProperties(currentNamespace, members);
            }

            return currentNamespace;
        }

        function defineNS(name, members) {
            return defineWithParent(global, name, members);
        }

        function markSupportedForProcessing() {
            return {
                value: function (func) {
                    func.supportedForProcessing = true;
                    return func;
                },
                configurable: false,
                writable: false,
                enumerable: true
            }
        }

        function define(constructor, instanceMembers, staticMembers) {
            constructor = constructor || function () { };
            markSupportedForProcessing(constructor);
            if (instanceMembers) {
                initializeProperties(constructor.prototype, instanceMembers);
            }
            if (staticMembers) {
                initializeProperties(constructor, staticMembers);
            }
            return constructor;
        }


        // Establish members of the "SP2013" namespace
        Object.defineProperties(_rootNamespace, {
            defineWithParent: {
                value: defineWithParent,
                writable: true,
                enumerable: true,
                configurable: true
            },
            defineNS: {
                value: defineNS,
                writable: true,
                enumerable: true,
                configurable: true
            },
            define: {
                value: define,
                writable: true,
                enumerable: true,
                configurable: true
            }
        });
    })("SP2013");

    //常用方法
    (function (namespace) {
        var dateFormat = function () {
            var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
                timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
                timezoneClip = /[^-+\dA-Z]/g,
                pad = function (val, len) {
                    val = String(val);
                    len = len || 2;
                    while (val.length < len) val = "0" + val;
                    return val;
                };

            // Regexes and supporting functions are cached through closure
            return function (date, mask, utc) {
                var dF = dateFormat;

                // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
                if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
                    mask = date;
                    date = undefined;
                }

                // Passing date through Date applies Date.parse, if necessary
                date = date ? new Date(date) : new Date;
                if (isNaN(date)) return ""; // throw SyntaxError("invalid date");
                if (date.getTime() == -62135596800000) return ""; //min value in c#

                mask = String(dF.masks[mask] || mask || dF.masks["default"]);

                // Allow setting the utc argument via the mask
                if (mask.slice(0, 4) == "UTC:") {
                    mask = mask.slice(4);
                    utc = true;
                }

                var _ = utc ? "getUTC" : "get",
                    d = date[_ + "Date"](),
                    D = date[_ + "Day"](),
                    m = date[_ + "Month"](),
                    y = date[_ + "FullYear"](),
                    H = date[_ + "Hours"](),
                    M = date[_ + "Minutes"](),
                    s = date[_ + "Seconds"](),
                    L = date[_ + "Milliseconds"](),
                    o = utc ? 0 : date.getTimezoneOffset(),
                    flags = {
                        d: d,
                        dd: pad(d),
                        ddd: dF.i18n.dayNames[D],
                        dddd: dF.i18n.dayNames[D + 7],
                        m: m + 1,
                        mm: pad(m + 1),
                        mmm: dF.i18n.monthNames[m],
                        mmmm: dF.i18n.monthNames[m + 12],
                        yy: String(y).slice(2),
                        yyyy: y,
                        h: H % 12 || 12,
                        hh: pad(H % 12 || 12),
                        H: H,
                        HH: pad(H),
                        M: M,
                        MM: pad(M),
                        s: s,
                        ss: pad(s),
                        l: pad(L, 3),
                        L: pad(L > 99 ? Math.round(L / 10) : L),
                        t: H < 12 ? "a" : "p",
                        tt: H < 12 ? "am" : "pm",
                        T: H < 12 ? "A" : "P",
                        TT: H < 12 ? "AM" : "PM",
                        Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                        o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                        S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
                    };

                return mask.replace(token, function ($0) {
                    return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
                });
            };
        }();

        // Some common format strings
        dateFormat.masks = {
            //"default": "ddd mmm dd yyyy HH:MM:ss",
            "default": "yyyy/mm/dd HH:MM",
            shortDate: "m/d/yy",
            mediumDate: "mmm d, yyyy",
            longDate: "mmmm d, yyyy",
            fullDate: "dddd, mmmm d, yyyy",
            shortTime: "h:MM TT",
            mediumTime: "h:MM:ss TT",
            longTime: "h:MM:ss TT Z",
            isoDate: "yyyy/mm/dd",
            isoTime: "HH:MM:ss",
            isoDateTime: "yyyy/mm/dd'T'HH:MM:ss",
            isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
        };

        // Internationalization strings
        dateFormat.i18n = {
            dayNames: [
                "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
            ],
            monthNames: [
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
            ]
        }

        // For convenience...
        Date.prototype.formatDate = function (mask, utc) {
            return dateFormat(this, mask, utc);
        }


        var queryString = (function () {
            var urlParams = {};
            var e,
                a = /\+/g, // Regex for replacing addition symbol with a space
                r = /([^&=]+)=?([^&]*)/g,
                d = function (s) {
                    return decodeURIComponent(s.replace(a, " "));
                },
                q = window.location.search.substring(1);

            while (e = r.exec(q))
                urlParams[d(e[1])] = d(e[2]);

            return urlParams;
        })();

        function concatUrl(base, url) {
            if (SP2013.Utility.isNullorEmpty(base))
                return url;

            if (SP2013.Utility.isNullorEmpty(url))
                return base;

            if (base.charAt(base.length - 1) == "/") {
                if (url.charAt(0) == "/")
                    return base + url.substring(1);
                else
                    return base + url;
            } else {
                if (url.charAt(0) == "/")
                    return base + url;
                else
                    return base + "/" + url;
            }
        }

        SP2013.defineNS(namespace, {
            queryString: queryString,
            formatDate: dateFormat,
            formateFloatDate: function (val, mask, utc) {
                var re = /-?\d+/;
                var m = re.exec(val);
                var d = new Date(parseInt(m));
                return dateFormat(d, mask, utc);
            },
            isNullorEmpty: function (str) {
                return str == null || $.trim(str).length == 0;
            },
            strEllipsis: function (str, n) {
                var ilen = str.length;
                if (ilen * 2 <= n) return str;
                n -= 3;
                var i = 0;
                while (i < ilen && n > 0) {
                    if (escape(str.charAt(i)).length > 4) n--;
                    n--;
                    i++;
                }
                if (n > 0) return str;
                return str.substring(0, i) + "...";
            },
            strWhiteSpace: function (str, n) {
                var ilen = str.length;
                if (ilen * 2 <= n) return str;
                n -= 3;
                var i = 0;
                while (i < ilen && n > 0) {
                    if (escape(str.charAt(i)).length > 4) n--;
                    n--;
                    i++;
                }
                if (n > 0) return str;
                return str.substring(0, i);
            },
            strSubTitle: function (str, n) {
                var ilen = str.length;
                if (ilen * 2 <= n) return "";
                n -= 3;
                var i = 0;
                while (i < ilen && n > 0) {
                    if (escape(str.charAt(i)).length > 4) n--;
                    n--;
                    i++;
                }
                if (n > 0) return "";
                return str;
            },
            strSubLink: function (str) {
                if (str && str.indexOf(',') > -1) {
                    return str.split(',')[0];
                }
                else
                    return str;
            },
            dateFromService: function (d) {
                return eval("new " + d.slice(1, -1));
            },
            XMLToString: function (oXML) {
                if (window.ActiveXObject) {
                    return oXML.xml;
                } else {
                    return (new XMLSerializer()).serializeToString(oXML);
                }
            },
            XMLFromString: function (sXML) {
                if (window.ActiveXObject) {
                    var oXML = new ActiveXObject("Microsoft.XMLDOM");
                    oXML.loadXML(sXML);
                    return oXML;
                } else {
                    return (new DOMParser()).parseFromString(sXML, "text/xml");
                }
            },
            isUrl: function (str) {
                if (this.isNullorEmpty(str))
                    return false;

                //var expression = /[-a-zA-Z0-9@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?/gi;
                var expression = "[a-zA-z]+://[^\s]*";
                var regex = new RegExp(expression);
                return str.match(regex);
            },
            json2str: function (obj) {
                return JSON ? JSON.stringify(obj) : this.obj2str(obj);
            },
            obj2str: function (o) {
                if (o == null)
                    return "\"\"";
                var r = [];
                if (typeof o == "string") return "\"" + o.replace(/([\'\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
                if (o instanceof Date) return "\"\\\/Date(" + o.getTime() + ")\\\/\"";
                if (typeof o == "object") {
                    if (!o.sort) {
                        for (var i in o)
                            r.push("\"" + i + "\":" + obj2str(o[i]));
                        if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                            r.push("toString:" + o.toString.toString());
                        }
                        r = "{" + r.join() + "}"
                    } else {
                        for (var i = 0; i < o.length; i++)
                            r.push(obj2str(o[i]))
                        r = "[" + r.join() + "]"
                    }
                    return r;
                }
                return o.toString();
            },
            ReturnJson: function (obj) {
                var dtd = $.Deferred();
                obj.then(function (result) {
                    if (result.IsError) {
                        dtd.resolve();
                    } else {
                        dtd.resolve(result.Data);
                    }
                }, function (jqXHR, textStatus, errorThrown) {
                    dtd.resolve();
                });
                return dtd.promise();
            },
            ReturnAPIJson: function (obj) {
                var dtd = $.Deferred();
                obj.then(function (result) {
                    if (result) {
                        dtd.resolve(result.d.results);
                    }
                }, function (jqXHR, textStatus, errorThrown) {
                    SP2013.UI.ajaxError(jqXHR, textStatus, errorThrown);
                    dtd.resolve();
                });
                return dtd.promise();
            },
            concatUrl: function (urls) {
                var url = "";
                if (!arguments || arguments.length == 0) {

                } else {
                    for (var i = 0; i < arguments.length; i++) {
                        url = concatUrl(url, arguments[i]);
                    }
                }
                return url;
            },
            replaceHost: function (str) {
                if (str) {
                    var hostpath = window.location.protocol + "//" + window.location.host;
                    if (str.startsWith("http://" + window.location.host) || str.startsWith("https://" + window.location.host) || str.startsWith("http://10.10.20.6")) {
                        var newstr = str.replace(/http:\/\/[^\/?]*|https:\/\/[^\/?]*/, hostpath);
                        return newstr;
                    }
                    else {
                        return str;
                    }
                }
                else {
                    return '';
                }
            }
        });
    })("SP2013.Utility");

    //sharepoint ecmascript
    (function (namespace) {

        var ctx = SP2013.define(function (context) {
            this.context = context;
            this.web = null;
        }, {
            execute: function () {
                var self = this;
                var dtd = $.Deferred();
                self.context.executeQueryAsync(
                    Function.createDelegate(this, function () {
                        dtd.resolve();
                    }),
                    Function.createDelegate(this, function (sender, args) {
                        dtd.reject(sender, args);
                    })
                );
                return dtd.promise();
            },
            load: function (obj) {
                var self = this;
                var dtd = $.Deferred();

                self.context.load(obj);
                self.context.executeQueryAsync(
                    Function.createDelegate(this, function () {
                        dtd.resolve(obj);
                    }),
                    Function.createDelegate(this, function (sender, args) {
                        dtd.reject(sender, args);
                    })
                );

                return dtd.promise();
            },
            loadWeb: function (force) {

                var self = this;
                var dtd = $.Deferred();
                if (self.web && !force) {
                    dtd.resolve(self.web);
                } else {
                    self.load(self.web ? self.web : self.context.get_web()).then(function (web) {
                        self.web = web;
                        dtd.resolve(self.web);
                    }, function () {
                        dtd.reject();
                    });
                }
                return dtd.promise();
            },
            setMasterPage: function (url, check) {
                var self = this;
                var dtd = $.Deferred();
                self.loadWeb().done(function (web) {

                    if (check) {
                        if (web.get_masterUrl() == url) {
                            dtd.resolve(false);
                        }
                    }

                    web.set_masterUrl(url);
                    web.update();
                    self.loadWeb(true).then(function () {
                        dtd.resolve(true);
                    }, function () {
                        dtd.reject();
                    });
                });

                return dtd.promise();
            },
            getContentTypeById: function (id) {
                var self = this;
                var dtd = $.Deferred();
                self.loadWeb().done(function (web) {
                    self.load(web.get_contentTypes().getById(id)).done(function (type) {
                        dtd.resolve(type);
                    }).fail(function () {
                        dtd.reject()
                    });
                });
                return dtd.promise();
            },
            setFormOfContentType: function (id, newForm, editForm, displayForm) {
                var self = this;
                var dtd = $.Deferred();

                self.getContentTypeById(id).done(function (type) {
                    var changed = false;

                    if (newForm) {
                        var old = type.get_newFormUrl();
                        if (SP2013.Utility.isNullorEmpty(old) || old != newForm) {
                            type.set_newFormUrl(newForm);
                            changed = true;
                        }
                    }
                    if (editForm) {
                        var old = type.get_editFormUrl();
                        if (SP2013.Utility.isNullorEmpty(old) || old != editForm) {
                            type.set_editFormUrl(editForm);
                            changed = true;
                        }
                    }
                    if (displayForm) {
                        var old = type.get_displayFormUrl();
                        if (SP2013.Utility.isNullorEmpty(old) || old != displayForm) {
                            type.set_displayFormUrl(displayForm);
                            changed = true;
                        }
                    }
                    if (changed) {
                        type.update(true);
                        self.load(type).done(function () {
                            dtd.resolve(true);
                        });
                    } else {
                        dtd.resolve(false);
                    }
                }).fail(function () {
                    dtd.reject();
                });
                return dtd.promise();
            },
            getListByTitle: function (title) {
                return this.load(this.context.get_web().get_lists().getByTitle(title));
            },
            getListById: function (id) {
                return this.load(this.context.get_web().get_lists().getById(id));
            },
            getwithproxy: function (url) {
                var request = new SP.WebRequestInfo();
                request.set_url(url);
                request.set_method("GET");
                request.set_headers({ "Accept": "application/json" });
                var response = SP.WebProxy.invoke(this.context, request);
                var dtd = $.Deferred();
                this.context.executeQueryAsync(
                    Function.createDelegate(this, function () {
                        var ResponseBody = JSON.parse(response.get_body());
                        dtd.resolve(ResponseBody);
                    }),
                    Function.createDelegate(this, function (sender, args) {
                        dtd.reject(sender, args);
                    })
                );
                return dtd.promise();
            },
            setLike: function (listid, itemid, islike) {
                var aContextObject = this.context;
                var dtd = $.Deferred();
                EnsureScriptFunc('reputation.js', 'Microsoft.Office.Server.ReputationModel.Reputation', function () {
                    Microsoft.Office.Server.ReputationModel.Reputation.setLike(aContextObject,
                        listid,
                        itemid, islike);
                    aContextObject.executeQueryAsync(
                        function (data) {
                            dtd.resolve(data);
                        }, function (sender, args) {
                            dtd.reject(sender, args);
                        });
                });
                return dtd.promise();
            },
            getLikeCount: function (listid, itemid) {
                var acontext = this.context;
                var list = acontext.get_web().get_lists().getById(listid);
                var item = list.getItemById(itemid);
                var dtd = $.Deferred();
                acontext.load(item, "LikedBy", "ID", "LikesCount");
                acontext.executeQueryAsync(Function.createDelegate(this, function (success) {
                    var likeDisplay = true;
                    var itemc = item.get_item('LikesCount');
                    dtd.resolve(itemc);
                }), Function.createDelegate(this, function (sender, args) {
                    dtd.reject(sender, args);
                }));
                return dtd.promise();
            },
            GetUserPermission: function (listName) {
                var acontext = this.context;
                var spList = acontext.get_web().get_lists().getByTitle(listName);
                acontext.load(spList, 'EffectiveBasePermissions');
                var dtd = $.Deferred();
                acontext.executeQueryAsync(
                    // OnSuccess
                    function (sender, args) {
                        var listItem_HasEditPerms = spList.get_effectiveBasePermissions().has(SP.PermissionKind.editListItems);
                        dtd.resolve(listItem_HasEditPerms);
                    },
                    // OnFailure
                    function (sender, args) {
                        dtd.reject(sender, args);
                    }
                );
                return dtd.promise();
            },
            GetUserFolderPermission: function (FolderRelativeUrl) {
                var acontext = this.context;
                var oWebsite = acontext.get_web();
                var folder = oWebsite.getFolderByServerRelativeUrl(oWebsite.get_serverRelativeUrl() + FolderRelativeUrl);
                acontext.load(folder);
                var dtd = $.Deferred();
                acontext.executeQueryAsync(
                    // OnSuccess
                    function (sender, args) {
                        var list = folder.get_lists();
                        var item = list.getItemById(folder.id);
                        var listItem_HasEditPerms = item.get_effectiveBasePermissions().has(SP.PermissionKind.editListItems);
                        dtd.resolve(listItem_HasEditPerms);
                    },
                    // OnFailure
                    function (sender, args) {
                        dtd.reject(sender, args);
                    }
                );
                return dtd.promise();
            }
        }, {

        });

        SP2013.defineNS(namespace, {
            Context: ctx
        });

    })("SP2013");

    //
    (function (namespace) {
        var _currentWeb = "";

        //获取当前站点site url
        function getCurrentWeb() {
            _currentWeb = _spPageContextInfo.webServerRelativeUrl;
            return _currentWeb;
        }

        //获取当前站点site url
        function getCurrentWebAbsoluteUrl() {
            _currentWeb = _spPageContextInfo.webAbsoluteUrl;
            return _currentWeb;
        }

        //发起api请求
        function APIGet(method, data, siteUrl, async, headers) {
            var options = {
                async: async,
                contentType: 'application/json',
                dataType: 'json',
                headers: $.extend({
                    'accept': "application/json;odata=verbose"
                }, headers),
                type: 'GET'
            }

            if (typeof method === 'object') {
                $.extend(options, method);
            } else {
                $.extend(options, {
                    url: getAPIUrl(siteUrl, method),
                    data: data ? bps360.Utility.json2str(data) : null
                });
            }
            return $.ajax(options);
        }

        //发起服务器请求
        function callJSON(method, data, type, siteUrl) {
            var options = {
                contentType: 'application/json',
                dataType: 'json'
            };
            if (typeof method === 'object') {
                $.extend(options, method);
            } else {
                var site = siteUrl || getCurrentWeb();
                $.extend(options, {
                    url: SP2013.Utility.concatUrl(site, method),
                    data: SP2013.Utility.json2str(data),
                    type: type
                });
            }
            return $.ajax(options);
        }

        //发起Jsonp服务器请求
        function callJSONP(method) {
            var options = {
                contentType: 'application/json',
                dataType: 'jsonp',
                jsonp: "callback",
            };
            $.extend(options, {
                url: method
            });

            return $.ajax(options);
        }

        //生成api请求地址
        function getAPIUrl(siteUrl, method) {
            var url = siteUrl || getCurrentWeb();
            return SP2013.Utility.concatUrl(url, "/_api/", method);
        }

        function getCurrentLanguage() {
            var _currentlang = _spPageContextInfo.currentLanguage;
            if ($.cookie("Lang")) {
                _currentlang = $.cookie("Lang");
            }
            return _currentlang == '1033' ? '1033' : '2025';
        }

        function getResourceLanguage(opts)
        {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/GetResourceId";
            return SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                model: opts.model,
                key: opts.key,
                lang: SP2013.O365.currentLang()
            });
        }

        SP2013.defineNS(namespace, {
            currentWebUrl: getCurrentWeb,
            currentWebAbsoluteUrl: getCurrentWebAbsoluteUrl,
            currentLang: getCurrentLanguage,
            resourceId: getResourceLanguage,
            getJSON: function (method, data, siteUrl) {
                var site = siteUrl || getCurrentWeb();
                var thisurl = SP2013.Utility.concatUrl(site, method);
                if (thisurl.indexOf('?') < 0) {
                    thisurl += '?';
                }
                thisurl += '&random=' + (new Date()).getTime();
                return $.getJSON(thisurl, data);
            },
            getJSONP: function (method) {
                return callJSONP(method);
            },
            putJSON: function (method, data, siteUrl) {
                return callJSON(method, data, 'PUT', siteUrl);
            },
            deleteJSON: function (method, data, siteUrl) {
                return callJSON(method, data, 'DELETE', siteUrl);
            },
            postJSON: function (method, data, siteUrl) {
                return callJSON(method, data, 'POST', siteUrl);
            },
            apiGet: APIGet
        });

    })("SP2013.O365");


    (function (namespace) {
        var msg_systemError = "System error, please contact IT helpdesk.";

        SP2013.defineNS(namespace, {
            executeQueryError: function (sender, args) {
                if (args != null) toastr.error(args.get_message(), "System Error!");
                else toastr.error(msg_systemError);
            },
            ajaxError: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR && jqXHR.responseText) {
                    var err = $.parseJSON(jqXHR.responseText);
                    toastr.error(err.Message);
                } else if (errorThrown) {
                    toastr.error(errorThrown);
                } else {
                    toastr.error(msg_systemError);
                }
            },
            spRESTError: function (error) {
                if (typeof error === 'string') {
                    toastr.error(error);
                } else if (error && error.responseText) {
                    if (typeof error.responseText === 'string') {
                        var err = $.parseJSON(error.responseText);
                        toastr.error(err.error.message.value);
                    }
                } else {
                    toastr.error(msg_systemError);
                }
            },
            error: function (msg, title) {
                //toastr.error(msg, title);
                $.messager.alert('Error', msg, 'error');
            },
            info: function (msg, title) {
                //toastr.info(msg, title);
                $.messager.alert('Info', msg, 'info');
            },
            success: function (msg, title) {
                //toastr.success(msg, title);
                $.messager.alert('Success', msg, 'info');
            },
            warning: function (msg, title) {
                //toastr.warning(msg, title);
                $.messager.alert('Warning', msg, 'warning');
            }
        });

    })("SP2013.UI");

})(this);
