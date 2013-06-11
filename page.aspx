<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/jqLab.Master" AutoEventWireup="true" CodeBehind="page.aspx.cs" Inherits="jqLab.page" %>

<%@ Register Src="~/Controls/Contents/Contents.ascx" TagPrefix="uc1" TagName="Contents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Contents runat="server" ID="Contents" />
</asp:Content>
