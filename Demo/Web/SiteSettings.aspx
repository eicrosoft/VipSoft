<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="Demo.SiteSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Button runat="server" ID="btnSetValue" Text="Set Value" OnClick="btnSetValue_Click"/>
     <asp:Button runat="server" ID="btnGetValue" Text="Get Value" OnClick="btnGetValue_Click"/>
        <asp:Button runat="server" ID="btnUpdate" Text="Update DataBase" OnClick="btnUpdate_Click"/>
        <br/>
        <asp:TextBox runat="server" ID="txtSiteName"></asp:TextBox>
        <br/>

    <div id="divCell" runat="server">
    
    </div>
    </form>
</body>
</html>
