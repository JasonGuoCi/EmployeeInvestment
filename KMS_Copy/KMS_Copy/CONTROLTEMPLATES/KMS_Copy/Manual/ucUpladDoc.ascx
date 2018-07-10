<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUpladDoc.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Manual.ucUpladDoc" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div id="Upload" class="modal fade bs-example-modal-lb" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-describedby="myModalLabel">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dimiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;
                    </span>
                </button>
                <h4 class="modal-title" id="myModalLabel" data-bind="text: SP2013.O365.currentLang() == '1033' ? 'Upload' : '上传附件'">Upload</h4>
            </div>
            <div class="modal-body" style="padding: 10px;">
                <input id="file_upload" name="file_upload" type="file" multiple="multiple" />
            </div>

            <div id="fileQueue">
            </div>
            <p style="padding: 15px;" id="psubmit">
                &nbsp;<a id="acancel" href="javascript:$('#file_upload').uploadify('cancel','*')">取消上传</a>
            </p>
        </div>
    </div>
</div>
<div id="Uploadimage" class="modal fade bs-example-la" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-labelledby="yModalimage">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalimage" data-bind="text: SP2013.O365.currentLang() == '1033' ? 'Upload Image' : '上传图片'">Upload</h4>
            </div>
            <div class="modal-body" style="padding: 10px;">
                <input id="image_upload" name="image_upload" type="file" multiple="multiple" />
            </div>
            <div id="imageQueue"></div>
            <p style="padding: 15px;" id="pisubmit">
                &nbsp;<a id="imagecancel" href="javascript:$('#image_upload').uploadify('cancel''*')">取消上传</a>
            </p>
        </div>
    </div>
</div>

<script type="text/javascript">
    jQuery(document).ready(function () {
        var files = false;
        setTimeout(function () {
            var imgurl = '/_layouts/15/KMS_Copy/Images/browse-btn-eng.png';
            var errmsg = ['文件名称重复', '文件大小超出限制(不能超出250M)', '异常信息', '加载中...', '内容不能为空'];
            if (SP2013.O365.currentLang() == '1033') {
                imgurl = '/_layouts/15/KMS_Copy/Images/browse-btn.png';
                errmsg = ['File Name is Exist', 'File size limit exceeded(Cannot exceed 250M)', 'Abnormal information', 'Please wait...', 'The content cannot be empty'];
            }
            $("#file_upload").uploadify({
                'swf': '/_layouts/15/KMS_Copy/Scripts/uploadify/uploadify.swf',//swf的相对路径，必写项
                'uploader': _spPageContextInfo.webAbsoluteUrl + '/_layouts/15/UploadMoreFile.ashx', //服务器端脚本文件路径，必写项
                'buttonText': 'upload', //设置按钮文字。值会被当作html渲染，所以也可以包含html标签
                'cancelImage': '/_layouts/15/KMS_Copy/uploadify/uploadify-cancel.png',
                'buttonImage': imgurl, //设置图片按钮的路径（当你的按钮是一张图片时）。如果使用默认的样式，你还可以创建一个鼠标悬停状态，但要把两种状态的图片放在一起，并且默认的放上面，悬停状态的放在下面（原文好难表达啊：you can create a hover state for the button by stacking the off state above the hover state in the image）。这只是一个比较便利的选项，最好的方法还是把图片写在CSS里面。
                'formData': { 'ListName': 'Attachment', 'siteUrl': SP2013.O365.currentWebAbsoluteUrl() },//通过get或post上传文件时，此对象提供额外的数据。
                'fileTypeExts': '*.*',//设置允许上传的文件扩展名（也就是文件类型）。但手动键入文件名可以绕过这种级别的安全检查，所以你应该始终在服务端中检查文件类型。输入多个扩展名时用分号隔开('*.jpg;*.png;*.gif')
                'fileTypeDesc': '请选择正确的文件',  //可选文件的描述。这个值出现在文件浏览窗口中的文件类型下拉选项中。（chrome下不支持，会显示为'自定义文件',ie and firefox下可显示描述）
                'queueID': 'fileQueue', //设置上传队列DOM元素的ID，上传的项目会增加进这个ID的DOM中。设置为false时则会自动生成队列DOM和ID。默认为false
                'auto': true, //接受true or false两个值，当为true时选择文件后会自动上传；为false时只会把选择的文件增加进队列但不会上传，这时只能使用upload的方法触发上传。不设置auto时默认为true
                'multi': true,//设置是否允许一次选择多个文件，true为允许，false不允许
                'debug': false,//开启或关闭debug模式
                'successTimeout': 99999, //设置文件上传后等待服务器响应的秒数，超出这个时间，将会被认为上传成功，默认为30秒
                'fileSizeLimit': 250 * 1024 - 1,//设置上传文件的容量最大值。这个值可以是一个数字或者字符串。如果是字符串，接受一个单位（B,KB,MB,GB）。如果是数字则默认单位为KB。设置为0时表示不限制
                'onQueueComplete': function (stats) {//当队列中的所有文件全部完成上传时触发onUploadSuccess
                    //队列中的文件都上传完后触发，返回queueDate参数，有以下属性：
                    /*
                        uploadsSuccessful 成功上传的文件数量
                        uploadsErrored 出现错误的文件数量
                    */
                    $("#Upload").modal('hide');
                    $("#psubmit").show();
                },
                'onUploadSuccess': function (file, response, states) {//每个文件上传成功后触发   
                    var redata = eval('(' + response + ')');
                    if (redata.isError) {
                        if (redata.Msg != "") {
                            alert("文件名" + redata.Msg + "已经存在,请重新修改文件名后上传!");
                        }
                        return;
                    }
                    else {
                        var str = file.name;
                        str = str.substring(str.lastIndexOf('.'));
                        var html = "";
                        if (str.toLowerCase() == ".mp4") {
                            html = "<a target=\"_blank\" href=\"" + SP2013.O365.currentWebAbsoluteUrl() + "/Pages/Projects/VideoPlayer.aspx?extSrc=" + SP2013.O365.currentWebAbsoluteUrl() + "/" + redata.FileUrl + "\" >" + redata.FileName + "</a><br/>";
                        }
                        else if (str.toLowerCase() == ".mp3") {
                            html = "<a target=\"_blank\" href=\"" + SP2013.O365.currentWebAbsoluteUrl() + "/Pages/Projects/VideoPlayer.aspx?extSrc=" + SP2013.O365.currentWebAbsoluteUrl() + "/" + redata.FileUrl + " &type=mp3\" >" + redata.FileName + "</a><br/>";
                        }
                        else {
                            html = "<a target=\"_blank\" href=\"" + SP2013.O365.currentWebAbsoluteUrl() + "/" + redata.FileUrl + "\" >" + redata.FileName + "</a><br/>";
                        }
                        tinymce.get(Common).execCommand('mceInsertContent', true, html);
                    }
                },
                'onSelectError': function (file, errorCode, errorMsg) {
                    //上传文件出错是触发（每个出错文件触发一次）
                    $("#psubmit").show();
                    var fileSize = Math.round(file.size / 1024);
                    if (fileSize == 0) {
                        alert(file.name + "" + errmsg[4]);
                    }
                },
                'onSelect': function (file) {//选择每个文件增加进队列时触发，返回file参数
                    files = true; error = true;
                    var Prompt = IsContain(file.name);
                    if (Prompt != "") {
                        alert(file.name + " " + "文件名不能包含下列字符：" + Prompt);
                        $("#flie_upload").uploadify('cancel', file.Id);
                        return;
                    }
                },
                'onClearQueue': function (queueItemCount) {
                    //当调用cancel方法且传入'*'这个参数的时候触发，其实就是移除掉整个队列里的文件时触发，上面有说cancel方法带*时取消整个上传队列
                    files = false;
                }

            });

            $('#image_upload').uploadify({
                'swf': '/_layouts/15/KMS_Copy/Scripts/uploadify/uploadify.swf',
                'uploader': _spPageContextInfo.webAbsoluteUrl + '/_layouts/15/UploadMoreFile.ashx',
                'buttonText': 'upload',
                'cancelImage': '/_layouts/15/KMS_Copy/uploadify/uploadify-cancel.png',
                'buttonImage': imgurl,
                'formData': { 'ListName': "Attachment", "siteUrl": SP2013.O365.currentWebAbsoluteUrl() },
                'fileTypeExts': '*.*',
                'fileTypeDesc': '请选择正确的文件',
                'queueID': 'ImageQueue',
                'auto': true,
                'multi': true,
                'debug': false,
                'successTimeout': 99999,
                'fileSizeLimit': 250 * 1024 - 1,
                'onQueueComplete': function (stats) {//当队列中的所有文件全部完成上传时触发onUploadSuccess
                    if (stats.uploadsSuccessful > 0) {
                        $("#Uploadimage").modal('hide');
                        $("#pisubmit").show();
                    }

                },
                'onUploadStart': function (file) {
                    var str = file.name;
                    str = str.substring(str.lastIndexOf('.'));
                    if (str.toLowerCase() != ".bmp" && str.toLowerCase() != ".png" && str.toLowerCase() != ".gif" && str.toLowerCase() != ".jpg" && str.toLowerCase() != ".jpeg") {  //根据后缀，判断是否符合图片格式
                        alert(SP2013.O365.currentLang() == "2052" ? "不是指定图片格式,重新选择" : "Please select a picture format file");

                        $('#image_upload').uploadify('cancel', '*');
                    }
                },
                'onUploadSuccess': function (file, response, states) {
                    var redata = eval('(' + response + ')');
                    if (redata.IsError) {
                        if (redata.Msg != "")
                            alert("文件名" + redata.Msg + "已经存在,请重新修改文件名后上传!");
                        return;
                    }
                    else {
                        var html = "<img style=\"max-width:600px\" src=\"" + SP2013.O365.currentWebAbsoluteUrl() + "/" + redata.FileUrl + "\" ></img><br/>";
                        tinymce.get(Common).execCommand('mceInsertContent', true, html);
                    }

                },
                'onSelectError': function (file, errorCode, errorMsg) {
                    //上传文件出错是触发（每个出错文件触发一次）
                    $("#pisubmit").show();
                    //上传文件出错是触发（每个出错文件触发一次）
                    var fileSize = Math.round(file.size / 1024);
                    if (fileSize == 0)
                        alert(file.name + "" + errmsg[4]);
                    //else if (errorCode = -110)
                    //    alert(file.name + "" + errmsg[1]);
                },
                'onSelect': function (file) {
                    //上传文件出错是触发（每个出错文件触发一次）
                    files = true; error = true;
                    var Prompt = IsContain(file.name);
                    if (Prompt != "") {
                        alert(file.name + " " + "文件名不能包含下列字符：" + Prompt);
                        //if (SP2013.O365.currentLang() == "2052") alert(file.name + " " + "文件名不能包含下列字符：" + Prompt);
                        //else if (SP2013.O365.currentLang() == "1033") alert(file.name + " " + "The file name is invalid or the file is empty. A file name cannot contain any of the following characters:" + Prompt);
                        $('#image_upload').uploadify('cancel', file.id);
                        return;
                    }
                },
                'onClearQueue': function (queueItemCount) {
                    files = false;
                }
            });

        }, 10);

        $("#aupload").click(function () {
            if (files) {
                $("#psubmit").hide();
                $('#file_upload').uploadify('upload', '*');
            }
            else {
                if (SP2013.O365.currentLang() == "2052") {
                    alert("请选择文件!");
                }
                else {
                    alert("Please select a document!");
                }

            }

        });
        $("#Upload").on('hide.bs.modal', function () {
            javascript: $('#file_upload').uploadify('cancel', '*');
        });

        $("#imageupload").click(function () {
            if (files) {
                $("#pisubmit").hide();
                $('#image_upload').uploadify('upload', '*');
            }
            else {
                if (SP2013.O365.currentLang() == "2052") {
                    alert("请选择文件!");
                }
                else {
                    alert("Please select a document!");
                }

            }

        });
        $('#Uploadimage').on('hide.bs.modal', function () {
            javascript: $('#image_upload').uploadify('cancel', '*');
        });

    });
    function IsContain(format) {
        var isShow = "";

        if (format.indexOf("\\") != -1)
            isShow += " " + "\\";
        if (format.indexOf("/") != -1)
            isShow += " " + "/";
        if (format.indexOf(":") != -1)
            isShow += " " + ":";
        if (format.indexOf("*") != -1)
            isShow += " " + "*";
        if (format.indexOf("?") != -1)
            isShow += " " + "?";
        if (format.indexOf("\"") != -1)
            isShow += " " + "\"";
        if (format.indexOf(">") != -1)
            isShow += " " + ">";
        if (format.indexOf("<") != -1)
            isShow += " " + "<";
        if (format.indexOf("|") != -1)
            isShow += " " + "|";
        if (format.indexOf("#") != -1)
            isShow += " " + "#";
        if (format.indexOf("{") != -1)
            isShow += " " + "{";
        if (format.indexOf("}") != -1)
            isShow += " " + "}";
        if (format.indexOf("%") != -1)
            isShow += " " + "%";
        if (format.indexOf("~") != -1)
            isShow += " " + "~";
        if (format.indexOf("&") != -1)
            isShow += " " + "&";
        if (format.indexOf("..") != -1)
            isShow += " " + "..";
        if (format.indexOf("+") != -1)
            isShow += " " + "+";
        if (format.indexOf(",") != -1)
            isShow += " " + ",";
        return isShow;
    }
</script>
