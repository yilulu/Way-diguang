<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="shangjialist.aspx.cs" Inherits="DTcms.Web.WebForm13" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>商家店鋪
    </div>
    <div id="ziye_middle">
        <div class="sjlb">
            <div id="ziye_middle_left_cs">
                <asp:Repeater ID="repdata" runat="server">
                    <ItemTemplate>
                        <ul class="cs_xia3_1" style="border-bottom: 1px dashed #95BC45">
                            <li class="cs_xia3_1_1"><a href="shangjiapro.aspx?userid=<%#Eval("id") %>" target="_blank">
                                <img src="<%#"upload/user/"+Eval("avatar") %>" name="cs_xia41" id="cs_xia41" width="310px"
                                    height="380px" /></a></li>
                            <li class="cs_xia3_1_2" style="clor: #293BA7">
                                <%#Eval("dianming")%></li><br />
                            <li class="cs_xia3_1_2" style="line-height: 24px;">
                                <%#Eval("dianmiaoshu")%></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="fenye_con_zzjs_1" class="fenye">
                <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                    <tr>
                        <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                            runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                            NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
                            SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                        </webdiyer:AspNetPager>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
