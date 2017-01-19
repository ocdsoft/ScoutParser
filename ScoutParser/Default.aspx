<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebParser._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <asp:Panel class="row" runat="server" DefaultButton="btnGo" Style="margin-top: 20px">
        <div class="col-md-12">
            <div class="form-group has-feedback">
                <input id="tbUrl" type="text" class="form-control" runat="server" />  
                <i class="form-control-feedback glyphicon glyphicon-search"></i>
                <asp:Button ID="btnGo" CssClass="hidden" runat="server" Text="Go" OnClick="btnGo_Click" />              
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <div class="col-md-12">
            <img id="image" runat="server" class="img-responsive" src="" />
            <h2 id="title" runat="server"></h2>
            <h4 id="author" runat="server"></h4>
            <h5 id="pubdate" runat="server"></h5>
            <div id="content" runat="server" style="width: auto; height: auto;"></div>
        </div>
    </div>

</asp:Content>
