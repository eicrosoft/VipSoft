<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionTest.aspx.cs" Inherits="Demo.TransactionTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btnSave" Text="Transaction Test" OnClick="btnSave_Click" />
            <br /><br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
