   M S B A M L   `   `   `  ????H  DWPF-LoginForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_LoginForm.ViewsWPF_LoginForm.Views  5      C&clr-namespace:WPF_LoginForm.ViewModelsWPF_LoginForm.ViewModels  6>   "    WPF_LoginForm.Views.HomeView   5      ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:WPF_LoginForm.Views     5      <	viewModel&clr-namespace:WPF_LoginForm.ViewModels     6>   .??5	      ? .+?5
   
   ?? .+?5      ?? $
??Home??6   ???#C1C1C1$??32??5      $??28(?5      $
??Left=?5      $??	10,0,0,20q?5      ?? 5      $??Center=?6   
?????? $??
Montserrat)?6O   $??20(?6g   $??	Birthdays??6u   ?? 5        ?? Orientation$  
Horizontal=?6   $	??498??62   .+?5      ?? $	??477??6   $	??200??5      
???????
???????$??1q?5       J? Clip 5      ?? 5       ?? RadiusX$ 10Q?6,    ?? RadiusY$ 10Q?5   ,    ?? Rect$ 0,0,480,600?5   ,   ._?5!      ? .+?5#      ?? .+?5%      &   System.Windows.Controls.DataGrid     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6'   
???????$??1q?5'         ColumnHeaderStyle 5(   "   ?? 5)   &    ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6,   .?5*   *   ??  ?? Property ? ??	 ?? Value	 ?#f0f0a36G   ?? 5+   *    ? ??
	 ?   ?6G   ?? 5,   *    ? ??$	 	10,0,0,10q?6D   ?? 5-   *    ? ??$	 
Montserrat)?6G   ?? 5.   *    ? ??$	 15(?6E   5/   '   
   Columns
 51   "   0  *System.Windows.Controls.DataGridTextColumn  52   &   ,  &System.Windows.Controls.DataGridColumn   Header$ 
First name??6o   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   Z?    ClipboardContentBinding 6L   Z? 6o     53   &   $ 	Last name??6o    69   Z?  6L   Z? 6o     54   &   $ Phone Number??6o    69   Z?  6L   Z? 6o     55   &   $	 Age??6o    69   Z?  6L   Z? 6o   57      58      59      5;      5=      ?? 5?      $	??500??6    ? 	Resources 5@      ??%X   '??       ?? 5A      " ??6   .?5B      ??  ? ??$	 15,0,0,0q?63   5C      5D      .+?5E      ?? $  
Horizontal=?6   $??0,10,0,0q?66   .+?5F      ?? .+?5G      ?? $??Upcoming Renewals??6$   $??28??5H      
???   ?$??20(?5J      $??Center=?5L   $   $??Center=?5M   $   ?? 5N      $	??200??6!   $	??150??5O      ???#f0f0a3
???????$??1q?5R       ?? CornerRadius$ 10??5S      $??0q?5T      ._?5U        $ * 6'   
???????$??1q?5W       5X   "   ?? 5Y   &   "  6,   .?5Z   *   ??  ? ??	 ?#f0f0a36G   ?? 5[   *    ? ??
	 ?   ?6G   ?? 5\   *    ? ??$	 	10,0,0,10q?6D   ?? 5]   *    ? ??$	 
Montserrat)?6G   ?? 5^   *    ? ??$	 15(?6E   5_   '   
 5a   "     5b   &   $ 
First name??6o    69   Z?  6L   Z? 6o     5c   &   $ 	Last name??6o    69   Z?  6L   Z? 6o   5e      5f      5h      ?? 5i      .+?5j      ?? $??	New Leads??6$   $??28??5k      
???   ?$??20(?5m      $??Center=?5n   $   $??Center=?5o   $   $??	0,00,0,00q?5p      ?? 5q      $	??265??6!   $	??150??5r      ???#f0f0a3
???????$??1q?5u      $ 10??5v      ._?5w        
???????$??1q?5y       5z   "   ?? 5{   &   "  6,   .?5|   *   ??  ? ??	 ?#f0f0a36G   ?? 5}   *    ? ??
	 ?   ?6G   ?? 5~   *    ? ??$	 1,0,0,1q?6D   ?? 5   *    ? ??$	 
Montserrat)?6G   ?? 5?   *    ? ??$	 15(?6E   5?   '   
 5?   "     5?   &   $ 
First name??6o    69   Z?  6L   Z? 6o     5?   &   $ 	Last name??6o    69   Z?  6L   Z? 6o     5?   &   $ Phone Number??6o    69   Z?  6L   Z? 6o   5?      5?      5?      5?      ?? 5?      $  
Horizontal=?6   5?      5?      5?      5?      5?      