<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Form2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .form-group { 
      display: flex; 
      margin-bottom: 10px; 
    }
        .form-label { 
      width: 150px;
      margin-left:650px;
      margin-right: 5px;
      margin-bottom:25px;
    }
        .form-input { 
      flex: 0.5; 
    }
       
    </style>
</head>
<body style="height: 100%; width: 100%; text-align:center; font-size:medium">
    <form id="form1" runat="server" style="width:100%; height:100%">
        <div><h1 style="text-align:center">Sign Up Form</h1>
             </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style2" Height="16px" Width="206px" placeholder="Enter your first name"></asp:TextBox></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label2" runat="server" Text="Middle Name"></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox2" runat="server" CssClass="auto-style3" Height="16px" Width="206px" placeholder="Enter your middle name"></asp:TextBox></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label3" runat="server" Text="Last Name"></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox3" runat="server" CssClass="auto-style2" Height="16px" Width="206px" placeholder="Enter your last name"></asp:TextBox></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label4" runat="server" Text="Phone No."></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox4" runat="server" TextMode="Number" Height="16px" Width="206px" placeholder="Enter your number"></asp:TextBox></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label10" runat="server" Text="Date Of Birth"></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox5" runat="server" TextMode="Date"  Height="16px" Width="206px" placeholder="Enter your dob"></asp:TextBox></span>
        </div></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label6" runat="server" Text="Email Id"></asp:Label></span>
            <span class="form-input"><asp:TextBox ID="TextBox6" runat="server" TextMode="Email" Height="16px" Width="206px" placeholder="Enter your email id"></asp:TextBox></span>
        </div>
        <div class="form-group">
    <span class="form-label"><asp:Label ID="Label9" runat="server" Text="Address"></asp:Label></span>
    <span class="form-input"><asp:TextBox ID="TextBox7" runat="server" Width="35%" Rows="10" TextMode="MultiLine" Height="16px"></asp:TextBox></span>
</div>
        <div class="form-group">
           <span class="form-label"><asp:Label ID="Label8" runat="server" Text="Sex"></asp:Label></span>
            <span class ="form-input"><asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged" Text="Male" /></span>
            <span class="form-input" ><asp:RadioButton ID="RadioButton2" runat="server" OnCheckedChanged="RadioButton2_CheckedChanged" Text="Female" /></span>
        </div>
        <div class="form-group">
            <span class="form-label"><asp:Label ID="Label7" runat="server" Text="Stream"></asp:Label></span>

           <span class="form-input"> <asp:DropDownList ID="DropDownList1" runat="server">
               <asp:ListItem>SELECT</asp:ListItem>
                <asp:ListItem>CSE</asp:ListItem>
                <asp:ListItem>CSIT</asp:ListItem>
                <asp:ListItem>MECH</asp:ListItem>
                <asp:ListItem>EE</asp:ListItem>
                <asp:ListItem>ECE</asp:ListItem>
                <asp:ListItem>CIVIL</asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList></span>
        </div>
       <div class="auto-style7">
            <asp:Button ID="Button1" OnClick="Button1_Click1" runat="server" Height="31px" Text="SIGN UP" Width="145px" />
        </div>
        <div>

            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
        </div>

            <br /> <br /> <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
    <Columns>
        <asp:TemplateField HeaderText="View">
    <ItemTemplate>
      <a href="WebForm2.aspx?id=<%# Eval("First Name") %>">View</a>
            </ItemTemplate>
</asp:TemplateField> 
        
        <%--<asp:TemplateField HeaderText="ID">   

            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
              --%>

        <asp:BoundField DataField="First Name" HeaderText="First Name" />
        <asp:BoundField DataField="Middle Name" HeaderText="Middle Name" />
        <asp:BoundField DataField="Last Name" HeaderText="Last Name" />
        <asp:BoundField   
 DataField="Phone No." HeaderText="Phone No." />
        <asp:BoundField DataField="Date of Birth"   
 HeaderText="Date of Birth" />
        <asp:BoundField DataField="Email ID" HeaderText="Email ID" />
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="Sex"   
 HeaderText="Sex" />
        <asp:BoundField DataField="Stream" HeaderText="Stream" />
    </Columns>
</asp:GridView>
        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Enabled="false" />
<asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" Enabled="false" />
    </form>
    
</body>
</html>