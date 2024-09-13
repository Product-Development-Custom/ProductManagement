<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ProductManagement.Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Management</title>
    <style>
        form {
            margin: 0 auto;
            width: 700px;
            background-color: #ffffff;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        label, input, button {
            display: block;
            margin-bottom: 5px;
        }

        label {
            font-weight: bold;
        }

        input[type="text"] {
            padding: 5px;
            font-size: 1em;
            border: 1px solid #ccc;
            border-radius: 4px;
            width: calc(100% - 20px);
        }

        .search-container {
            display: flex;
        }

        .Btn-container {
            display: flex;
            justify-content: space-around;
        }

        .btn-red {
            background-color: #ff0000;
            color: white;
            border: none;
            border-radius: 5px;
        }

        .btn-green {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-green:hover {
                background-color: #45a049;
            }

        .actions {
            display: flex;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="search-container">
                <asp:TextBox ID="txt_Search" runat="server" placeholder="Search category or username"></asp:TextBox>
                <asp:Button ID="btn_Search" runat="server" Text="Search" OnClick="btn_Search_Click" CssClass="btn-red" />
            </div>
            <br />
            <asp:Label runat="server" Text="Product Name"></asp:Label>
            <asp:TextBox ID="txt_ProductName" runat="server" placeholder="Enter product name"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Product Description"></asp:Label>
            <asp:TextBox ID="txt_description" runat="server" placeholder="Enter product description"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Category Name"></asp:Label>
            <asp:TextBox ID="txt_c_name" runat="server" placeholder="Enter category name"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="User Name"></asp:Label>
            <asp:TextBox ID="txt_Username" runat="server" placeholder="Enter user name"></asp:TextBox>
            <br />
            <div class="grid-container">
                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="actions">
                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="EditProduct" CommandArgument='<%# Eval("ProductID") %>' />
                                    <asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br>
            <div class="Btn-container">
                <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClick="btn_Submit_Click" CssClass="btn-green" />
                <asp:Button ID="btn_Load" runat="server" Text="Load" OnClick="btn_Load_Click" CssClass="btn-green" />
            </div>
        </div>
    </form>
</body>
</html>
