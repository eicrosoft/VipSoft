<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Demo.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptPaymentTerm" runat="server" OnItemDataBound="rptPaymentTerm_DataBound">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>Term</td>
                        <td>credit</td>
                        <td>echeck</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Term") %></td>
                    <td>
                        <input id="chkcredit" value='<%#Eval("Term")+"_1"%>' type="checkbox" runat="server" /><%#Eval("Term") %>_1
                    </td>
                    <td>
                        <input id="chkecheck" value='<%#Eval("Term")+"_2"%>' type="checkbox" runat="server" /><%#Eval("Term") %>_2
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <asp:Button ID="btn" Text="Save" OnClick="Save" runat="server" />
        <div id="divCellter" runat="server"></div>
    </div>
    </form>
</body>
</html>
