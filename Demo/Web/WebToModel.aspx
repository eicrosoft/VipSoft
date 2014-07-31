<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebToModel.aspx.cs" Inherits="Demo.WebToModel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" id="iUName" name="UName" />
        <asp:TextBox ID="txtUName" runat="server" />    <br />
        <asp:TextBox ID="Password" runat="server" />    <br />
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" /> <br />       
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
