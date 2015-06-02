<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ConnectMe.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="css/styles.css">
    <meta name="author" content="lithach">
    <link rel="shortcut icon" href="img/icons/logo.png">
    <script type="text/javascript" src="data.js"></script>
    <script type="text/javascript" src="display.js"></script>
    <script type="text/javascript" src="control.js"></script>
    <script src="//code.jquery.com/jquery-2.1.3.min.js"></script>

    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

    <!-- our own stylesheet for overrides -->
    <link rel="stylesheet" href="css/styles.css">
</head>



<body>
    <div id="title">
        <img class="msg" src="img/icons/logo.png">
    </div>
    <asp:Label ID="statusLabel" runat="server" Text=""></asp:Label>
    <div>
        <form id="mainForm" runat="server" class="form-horizontal">
            <div>
                <div class="form-group">
                    <asp:Button runat="server" ID="sampleUserBtn" class="btn btn-primary" Text="Sign In as SampleUser1" ToolTip="Sign In as SampleUser1" OnClick="sampleUserBtn_Click" />
                </div>
            </div>

            <div id="divider">
                OR
            </div>
            <div class="form-group">
                <asp:TextBox ID="emailBox" runat="server" class="form-control" placeholder="Email" TextMode="Email" />
                <br />
                <asp:TextBox runat="server" class="form-control" ID="passwordBox" placeholder="Password" TextMode="Password" />
                <br />
                <asp:Button runat="server" class="btn btn-primary" Text="Sign In" ID="submitBtn" OnClick="Unnamed1_Click" />
                <br />
                <asp:TextBox runat="server" class="form-control" ID="usernameBox" placeholder="Your username (only if creating account)" />
                <asp:Button runat="server" ID="signupBtn" class="btn btn-primary" OnClick="signupBtn_Click" Text="Create Account" ToolTip="Create Account" />
            </div>
        </form>
    </div>
    <div>
    </div>
</body>


</html>
