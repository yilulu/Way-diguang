<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="lunbo.ascx.cs" Inherits="DTcms.Web.lunbo" %>
<style type="text/css">
  
    /* qqshop focus */
    #focus
    {
        width: 1100px;
        height: 320px;
        overflow: hidden;
        position: relative;
    }
    #focus ul
    {
        height: 320px;
        position: absolute;
    }
    #focus ul li
    {
        float: left;
        width: 1100px;
        height: 320px;
        overflow: hidden;
        position: relative;
        background: #000;
    }
    #focus ul li div
    {
        position: absolute;
        overflow: hidden;
    }
    #focus .btnBg
    {
        position: absolute;
        width: 1100px;
        height: 20px;
        left: 0;
        bottom: 0;
        background: #000;
        display: none;
    }
    #focus:hover .btnBg
    {
        display: block;
    }
    #focus .btn
    {
        position: absolute;
        width: 1080px;
        height: 10px;
        padding: 5px 10px;
        right: 0;
        bottom: 0;
        text-align: right;
        display: none;
    }
    #focus:hover .btn
    {
        display: block;
    }
    #focus .btn span
    {
        display: inline-block;
        _display: inline;
        _zoom: 1;
        width: 25px;
        height: 10px;
        _font-size: 0;
        margin-left: 5px;
        cursor: pointer;
        background: #fff;
    }
    #focus .btn span.on
    {
        background: #fff;
    }
    #focus .preNext
    {
        width: 45px;
        height: 100px;
        position: absolute;
        top: 90px;
        background: url(img/sprite.png) no-repeat 0 0;
        cursor: pointer;
        display: none;
    }
    #focus .pre
    {
        left: 0;
    }
    #focus .next
    {
        right: 0;
        background-position: right top;
    }
</style>
<script src="../js/lunbo2.js" type="text/javascript"></script>
<div id="focus">
    <ul>
        <asp:Repeater ID="repdate" runat="server">
            <ItemTemplate>
                <li><a href='<%#Eval("link_url") %>' target="_blank">
                    <img width="1102" height="319" src='<%#Eval("img_url") %>' />
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
