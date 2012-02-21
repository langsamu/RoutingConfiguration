<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Sample.App_Pages.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Home</h1>
    </div>
    <ul>
        <li>
            <asp:HyperLink runat="server" Text="Home" NavigateUrl="<%$ RouteUrl: RouteName = Home %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test values" NavigateUrl="<%$ RouteUrl: RouteName = TestValues, segment1 = s1, segment2 = s2 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test values (using default)" NavigateUrl="<%$ RouteUrl:RouteName = TestValues, segment1 = s1 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test handler 1" NavigateUrl="<%$ RouteUrl: RouteName = TestHandler1, segment1 = s1, segment2= s2 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test handler 1 (default values)" NavigateUrl="<%$ RouteUrl: RouteName = TestHandler1, segment1 = s1 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Non-existent route (will be cought by catch-all route)" NavigateUrl="~/SomethingElse" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test constraints (foo, 2)" NavigateUrl="<%$ RouteUrl: RouteName = TestConstraints, segment1 = foo, segment2 = 2 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test constraints (bar, 4)" NavigateUrl="<%$ RouteUrl: RouteName = TestConstraints, segment1 = bar, segment2 = 4 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test constraints (foo, 1: shoudn't be a link)" NavigateUrl="<%$ RouteUrl: RouteName = TestConstraints, segment1 = foo, segment2 = 1 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test constraints (xxx, 2: shoudn't be a link)" NavigateUrl="<%$ RouteUrl: RouteName = TestConstraints, segment1 = xxx, segment2 = 2 %>" />
        </li>
        <li>
            <asp:HyperLink runat="server" Text="Test handler 2" NavigateUrl="<%$ RouteUrl: RouteName = TestHandler2, segment1 = s1 %>" />
        </li>
    </ul>
    </form>
</body>
</html>
