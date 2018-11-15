<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_config.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_config"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>系統參數設置</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        //表單驗證
        $(function () {
            $("#form1").validate({
                invalidHandler: function (e, validator) {
                    parent.jsprint("有 " + validator.numberOfInvalids() + " 項填寫有誤，請檢查！", "", "Warning");
                },
                errorPlacement: function (lable, element) {
                    //可見元素顯示錯誤提示
                    if (element.parents(".tab_con").css('display') != 'none') {
                        element.ligerTip({ content: lable.html(), appendIdTo: lable });
                    }
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 控制桌面 &gt; 系統參數設置</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">網站基本資料</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">功能權限配置</a></li>
            <li><a onclick="tabs('#contentTab',2);" href="javascript:void(0);">郵件發送配置</a></li>
            <li><a onclick="tabs('#contentTab',3);" href="javascript:void(0);">附件配置</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            站點名稱：
                        </th>
                        <td>
                            <asp:TextBox ID="webname" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司名稱：
                        </th>
                        <td>
                            <asp:TextBox ID="webcompany" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            會員管理密碼：
                        </th>
                        <td>
                            <asp:TextBox ID="weburl" runat="server" CssClass="txtInput normal required"
                                MaxLength="250" TextMode="Password"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            聯繫電話：
                        </th>
                        <td>
                            <asp:TextBox ID="webtel" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            傳真號碼：
                        </th>
                        <td>
                            <asp:TextBox ID="webfax" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            管理員郵箱：
                        </th>
                        <td>
                            <asp:TextBox ID="webmail" runat="server" CssClass="txtInput normal email" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            網站備案號：
                        </th>
                        <td>
                            <asp:TextBox ID="webcrod" runat="server" CssClass="txtInput normal" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            首頁標題(SEO)：
                        </th>
                        <td>
                            <asp:TextBox ID="webtitle" runat="server" CssClass="txtInput normal required" MaxLength="250"
                                Style="width: 350px;"></asp:TextBox><label>*自定義的首頁標題</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            頁面關鍵字(SEO)：
                        </th>
                        <td>
                            <asp:TextBox ID="webkeyword" runat="server" CssClass="txtInput" MaxLength="250" Style="width: 350px;"></asp:TextBox>
                            <label>
                                頁面關鍵字(keyword)</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            頁面描述(SEO)：
                        </th>
                        <td>
                            <asp:TextBox ID="webdescription" runat="server" MaxLength="250" TextMode="MultiLine"
                                CssClass="small"></asp:TextBox>
                            <label>
                                頁面描述(description)</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            網站版權信息：
                        </th>
                        <td>
                            <asp:TextBox ID="webcopyright" runat="server" MaxLength="500" TextMode="MultiLine"
                                CssClass="small"></asp:TextBox><label>支援HTML格式</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab_con">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            網站安裝目錄：
                        </th>
                        <td>
                            <asp:TextBox ID="webpath" runat="server" CssClass="txtInput normal required" MaxLength="100">/</asp:TextBox><label>*根目錄下，輸入“/”；如：http://abc.com/web，輸入“web/”</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            網站管理目錄：
                        </th>
                        <td>
                            <asp:TextBox ID="webmanagepath" runat="server" CssClass="txtInput normal required"
                                minlength="2" MaxLength="100">admin</asp:TextBox><label>*默認是admin，如已經更改，請輸入目錄名</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            URL重寫開關：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="staticstatus" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">關閉</asp:ListItem>
                                <asp:ListItem Value="1">偽URL重寫</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                                (<a href="url_rewrite_list.aspx">編輯偽靜態url替換規則</a>)</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            靜態URL後綴：
                        </th>
                        <td>
                            <asp:TextBox ID="staticextension" runat="server" CssClass="txtInput small required"
                                minlength="2" MaxLength="100"></asp:TextBox><label>*擴展名，不包括“.”，如：aspx、html</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            開啟會員功能：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="memberstatus" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0">關閉</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">開啟</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            開啟評論審核：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="commentstatus" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">關閉</asp:ListItem>
                                <asp:ListItem Value="1">開啟</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            後臺管理日誌：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="logstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">關閉</asp:ListItem>
                                <asp:ListItem Value="1">開啟</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否關閉網站：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="webstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="0">關閉</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">開啟</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            關閉原因描述：
                        </th>
                        <td>
                            <asp:TextBox ID="webclosereason" runat="server" MaxLength="500" TextMode="MultiLine"
                                CssClass="small"></asp:TextBox><label>支援HTML格式</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            網站統計代碼：
                        </th>
                        <td>
                            <asp:TextBox ID="webcountcode" runat="server" MaxLength="500" TextMode="MultiLine"
                                CssClass="small"></asp:TextBox><label>支援HTML格式</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab_con">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            STMP伺服器：
                        </th>
                        <td>
                            <asp:TextBox ID="emailstmp" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*發送郵件的SMTP伺服器地址</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            SMTP埠：
                        </th>
                        <td>
                            <asp:TextBox ID="emailport" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">25</asp:TextBox><label>*SMTP伺服器的埠</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            發件人地址：
                        </th>
                        <td>
                            <asp:TextBox ID="emailfrom" runat="server" CssClass="txtInput normal required" MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            郵箱帳號：
                        </th>
                        <td>
                            <asp:TextBox ID="emailusername" runat="server" CssClass="txtInput normal required"
                                MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            郵箱密碼：
                        </th>
                        <td>
                            <asp:TextBox ID="emailpassword" runat="server" CssClass="txtInput normal required"
                                MaxLength="100" TextMode="Password"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            發件人暱稱：
                        </th>
                        <td>
                            <asp:TextBox ID="emailnickname" runat="server" CssClass="txtInput normal required"
                                MaxLength="100"></asp:TextBox><label>*顯示發件人的昵稱</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab_con">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            附件上傳目錄：
                        </th>
                        <td>
                            <asp:TextBox ID="attachpath" runat="server" CssClass="txtInput normal required" minlength="2"
                                MaxLength="100">upload</asp:TextBox><label>*上傳圖片或附件的目錄，自動創建在網站根目錄下</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            附件上傳類型：
                        </th>
                        <td>
                            <asp:TextBox ID="attachextension" runat="server" CssClass="txtInput normal required"
                                MaxLength="250"></asp:TextBox><label>*以英文的逗號分隔開，如：“jpg,gif,rar”</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            附件儲存方式：
                        </th>
                        <td>
                            <asp:DropDownList ID="attachsave" runat="server" CssClass="select2">
                                <asp:ListItem Value="1">按年月日每天一個目錄</asp:ListItem>
                                <asp:ListItem Value="2">按年月/日/存入不同目錄</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            文件上傳大小：
                        </th>
                        <td>
                            <asp:TextBox ID="attachfilesize" runat="server" CssClass="txtInput small required number"
                                MaxLength="10"></asp:TextBox>KB<label>*超過設置的檔大小不予上傳，0不限制</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片上傳大小：
                        </th>
                        <td>
                            <asp:TextBox ID="attachimgsize" runat="server" CssClass="txtInput small required number"
                                MaxLength="10"></asp:TextBox>KB<label>*超過設置的圖片大小不予上傳，0不限制</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片最大尺寸：
                        </th>
                        <td>
                            <asp:TextBox ID="attachimgmaxheight" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>×
                            <asp:TextBox ID="attachimgmaxwidth" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>px
                            <label>
                                *設置圖片高和寬，超出自動裁剪，0為不受限制</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            編輯器圖片最大尺寸：
                        </th>
                        <td>
                            <asp:TextBox ID="Kindmaxheight" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>×
                            <asp:TextBox ID="Kindmaxwidth" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>px
                            <label>
                                *設置圖片高和寬，超出自動裁剪，0為不受限制</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            生成圖大小：
                        </th>
                        <td>
                            <asp:TextBox ID="thumbnailheight" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>×
                            <asp:TextBox ID="thumbnailwidth" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">0</asp:TextBox>px
                            <label>
                                *圖片生成縮圖高和寬，0為不生成</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片浮水印類型：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="watermarktype" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True">關閉浮水印 </asp:ListItem>
                                <asp:ListItem Value="1">文字浮水印 </asp:ListItem>
                                <asp:ListItem Value="2">圖片浮水印 </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片浮水印位置：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="watermarkposition" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">左上 </asp:ListItem>
                                <asp:ListItem Value="2">中上 </asp:ListItem>
                                <asp:ListItem Value="3">右上 </asp:ListItem>
                                <asp:ListItem Value="4">左中 </asp:ListItem>
                                <asp:ListItem Value="5">居中 </asp:ListItem>
                                <asp:ListItem Value="6">右中 </asp:ListItem>
                                <asp:ListItem Value="7">左下 </asp:ListItem>
                                <asp:ListItem Value="8">中下 </asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">右下 </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片生成品質：
                        </th>
                        <td>
                            <asp:TextBox ID="watermarkimgquality" runat="server" CssClass="txtInput small required digits"
                                MaxLength="3">80</asp:TextBox><label>*只適用於加浮水印的jpeg格式圖片.取值範圍 0-100, 0質量最低, 100質量最高,
                                    默認80</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            圖片浮水印文件：
                        </th>
                        <td>
                            <asp:TextBox ID="watermarkpic" runat="server" CssClass="txtInput normal required"
                                MaxLength="100">watermark.png</asp:TextBox><label>*需存放在站點目錄下，如圖片不存在將使用文字浮水印</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            浮水印透明度：
                        </th>
                        <td>
                            <asp:TextBox ID="watermarktransparency" runat="server" CssClass="txtInput small required digits"
                                MaxLength="2" max="10">5</asp:TextBox><label>*取值範圍1--10 (10為不透明)</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            浮水印文字：
                        </th>
                        <td>
                            <asp:TextBox ID="watermarktext" runat="server" CssClass="txtInput normal required"
                                MaxLength="100"></asp:TextBox><label>*文字浮水印的內容</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            文字字體：
                        </th>
                        <td>
                            <asp:DropDownList ID="watermarkfont" runat="server" CssClass="select2">
                                <asp:ListItem Value="Arial">Arial</asp:ListItem>
                                <asp:ListItem Value="Arial Black">Arial Black</asp:ListItem>
                                <asp:ListItem Value="Batang">Batang</asp:ListItem>
                                <asp:ListItem Value="BatangChe">BatangChe</asp:ListItem>
                                <asp:ListItem Value="Comic Sans MS">Comic Sans MS</asp:ListItem>
                                <asp:ListItem Value="Courier New">Courier New</asp:ListItem>
                                <asp:ListItem Value="Dotum">Dotum</asp:ListItem>
                                <asp:ListItem Value="DotumChe">DotumChe</asp:ListItem>
                                <asp:ListItem Value="Estrangelo Edessa">Estrangelo Edessa</asp:ListItem>
                                <asp:ListItem Value="Franklin Gothic Medium">Franklin Gothic Medium</asp:ListItem>
                                <asp:ListItem Value="Gautami">Gautami</asp:ListItem>
                                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                                <asp:ListItem Value="Gulim">Gulim</asp:ListItem>
                                <asp:ListItem Value="GulimChe">GulimChe</asp:ListItem>
                                <asp:ListItem Value="Gungsuh">Gungsuh</asp:ListItem>
                                <asp:ListItem Value="GungsuhChe">GungsuhChe</asp:ListItem>
                                <asp:ListItem Value="Impact">Impact</asp:ListItem>
                                <asp:ListItem Value="Latha">Latha</asp:ListItem>
                                <asp:ListItem Value="Lucida Console">Lucida Console</asp:ListItem>
                                <asp:ListItem Value="Lucida Sans Unicode">Lucida Sans Unicode</asp:ListItem>
                                <asp:ListItem Value="Mangal">Mangal</asp:ListItem>
                                <asp:ListItem Value="Marlett">Marlett</asp:ListItem>
                                <asp:ListItem Value="Microsoft Sans Serif">Microsoft Sans Serif</asp:ListItem>
                                <asp:ListItem Value="MingLiU">MingLiU</asp:ListItem>
                                <asp:ListItem Value="MS Gothic">MS Gothic</asp:ListItem>
                                <asp:ListItem Value="MS Mincho">MS Mincho</asp:ListItem>
                                <asp:ListItem Value="MS PGothic">MS PGothic</asp:ListItem>
                                <asp:ListItem Value="MS PMincho">MS PMincho</asp:ListItem>
                                <asp:ListItem Value="MS UI Gothic">MS UI Gothic</asp:ListItem>
                                <asp:ListItem Value="MV Boli">MV Boli</asp:ListItem>
                                <asp:ListItem Value="Palatino Linotype">Palatino Linotype</asp:ListItem>
                                <asp:ListItem Value="PMingLiU">PMingLiU</asp:ListItem>
                                <asp:ListItem Value="Raavi">Raavi</asp:ListItem>
                                <asp:ListItem Value="Shruti">Shruti</asp:ListItem>
                                <asp:ListItem Value="Sylfaen">Sylfaen</asp:ListItem>
                                <asp:ListItem Value="Symbol">Symbol</asp:ListItem>
                                <asp:ListItem Value="Tahoma" Selected="selected">Tahoma</asp:ListItem>
                                <asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
                                <asp:ListItem Value="Trebuchet MS">Trebuchet MS</asp:ListItem>
                                <asp:ListItem Value="Tunga">Tunga</asp:ListItem>
                                <asp:ListItem Value="Verdana">Verdana</asp:ListItem>
                                <asp:ListItem Value="Webdings">Webdings</asp:ListItem>
                                <asp:ListItem Value="Wingdings">Wingdings</asp:ListItem>
                                <asp:ListItem Value="仿宋_GB2312">仿宋_GB2312</asp:ListItem>
                                <asp:ListItem Value="宋體">宋體</asp:ListItem>
                                <asp:ListItem Value="新宋體">新宋體</asp:ListItem>
                                <asp:ListItem Value="楷體_GB2312">楷體_GB2312</asp:ListItem>
                                <asp:ListItem Value="黑體">黑體</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="watermarkfontsize" runat="server" CssClass="txtInput small2 required digits"
                                MaxLength="10">12</asp:TextBox>px
                            <label>
                                *文字浮水印的字體和大小</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="foot_btn_box">
            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
            &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
        </div>
    </div>
    </form>
</body>
</html>
