   M S B A M L   `   `   `  ????G  CTransfer_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Transfer_App.WindowsTransfer_App.Windows  5   	   %    Transfer_App.Windows.AddEditWnd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Transfer_App.Windows     5   	   <mah,http://metro.mahapps.com/winfx/xaml/controls     5   	     ? Title$4  .Добавити нового пасажира??5
   	   $	??415??6*   $	??321??67    ? WindowStartupLocation$ CenterScreen=?6C   $??Palatino Linotype)?5      $??15(?6$   $
??Bold"?62   .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??43* ?6   ?? 5      $	??68* ?6   ?? 5      $	??50* ?6   ?? 5      $	??10* ?6   ?? 5      $
??125* ?6   ?? 5      $	??13* ?6   ?? 5      $	??12* ?6    ? RowDefinitions 5   
   ?? 5      $	??27* ?6   ?? 5      $	??25* ?6   ?? 5      $??7* ?6   ?? 5      $	??22* ?6   ?? 5      $??7* ?6   ?? 5      $	??22* ?6   ?? 5      $??7* ?6   ?? 5      $	??22* ?6   ?? 5      $??7* ?6   ?? 5       $	??22* ?6   ?? 5!      $??7* ?6   ?? 5"      $	??22* ?6   ?? 5#      $??7* ?6   ?? 5$      $	??22* ?6   ?? 5%      $??7* ?6   ?? 5&      $	??22* ?6   ?? 5'      $??7* ?6   ?? 5(      $	??22* ?6   ?? 5)      $??7* ?6   ?? 5*      $	??22* ?6   ?? 5+      $??7* ?6   ?? 5,      $	??22* ?6   ?? 5-      $	??17* ?6   ?? 5.      $	??28* ?6   ?? 5/      $??9* ?6   .+?51   
   ?? -   5         ??Name$ gb??51      $??Add Form??6   $??25??61   $??7??6C   52      (  "System.Windows.Controls.DatePicker  53   
   -   5          Name$ 	bDate_txt??53      $??2??6(   $??1??68   $??4??6E   ?? 54   
   $??Booking Date:??6   $
??Left=?6(   $??19,0,0,0q?6C   $??Center=?6U   $??12(?6p   $??1??6~   $??2??6?   $??26??6?   $??89??6?   $??2??6?   ?? 55   
   -   5         ??Name$ 
bRoute_txt??55      $??3??6'   $??2??64   $??4??6D   ?? 56   
   $??Booking Route:??6   $
??Left=?6)   $??12,0,0,0q?6D   $??Center=?6V   $??12(?6q   $??3??6   $??26??6?   $??95??6?   $??2??6?   ?? 57   
   -   5         ??Name$ qty_txt??57       ?? TextWrapping$
 Wrap=?6#   $??5??67   $??2??6D   $??4??6T   ?? 58   
   $??	Quantity:??6   $??Center=?6$   $??Center=?6A   $??12(?6\   $??5??6j   $??26??6w   $??62??6?   $??1??6?   ?? 59   
   -   5        $ tax_txt??59      $
 Wrap=?6#   $??7??67   $??2??6D   $??4??6T   ?? 5:   
   $
??Tax:??6   $
??Left=?6   $??32,0,0,0q?6:   $??Center=?6L   $??12(?6g   	 J? RenderTransformOrigin$	 0.437,0.574&?6u   $??7??6?   $??26??6?   $??32??6?   $??2??6?   $??1??6?   ?? 5;   
   -   5        $ 	total_txt??5;      $
 Wrap=?6%   $??9??69   $??2??6F   $??4??6V   ?? 5<   
   $??Total:??6   $
??Left=?6!   $??23,0,0,0q?6<   $??Center=?6N   $??12(?6i   $??9??6w   $??26??6?   $??42??6?   $??1??6?   ?? 5=   
   -   5        $ pay_txt??5=      $
 Wrap=?6#   $??11??67   $??2??6E   $??4??6U   ?? 5>   
   $??Payment Method:??6   $??12(?6*   $??10??68   $??2,6,2,4q?6F   $??3??6W   $??2??6h   ?? 5?   
   -   5        $ 	fName_txt??5?      $
 Wrap=?6%   $??13??69   $??2??6G   $??4??6W   ?? 5@   
   $??First Name:??6   $
??Left=?6&   $??32,0,0,0q?6A   $??Center=?6S   $??12(?6n   $??13??6|   $??26??6?   $??73??6?   $??2??6?   ?? 5A   
   -	   5        $ 	lName_txt??5A      $
 Wrap=?6%   $??15??69   $??2??6G   $??4??6W   ?? 5B   
   $??
Last Name:??6   $
??Left=?6%   $??34,0,0,0q?6@   $??Center=?6R   $??12(?6m   $??15??6{   $??26??6?   $??71??6?   $??2??6?   ?? 5C   
   -
   5        $ 	phone_txt??5C      $
 Wrap=?6%   $??17??69   $??2??6G   $??4??6X   ?? 5D   
   $??Phone:??6   $
??Left=?6!   $??15,0,0,0q?6<   $??Center=?6N   $??12(?6i   $??17??6w   $??26??6?   $??48??6?   $??1??6?   ?? 5E   
   -   5        $ 	email_txt??5E      $
 Wrap=?6%   $??19??69   $??2??6G   $??4??6W   ?? 5F   
   $??Notes:??6   $
??Left=?6!   $??18,1,0,0q?6<   $	??Top=?6N   $??12(?6f   $??21??6t   $??26??6?   $??45??6?   $??2??6?   $??1??6?   ?? 5G   
   -   5        $ 	notes_txt??5G      $
 Wrap=?6%   $??21??69   $??2??6G   $??4??6W   ?? 5H   
   $??Email:??6   $
??Left=?6!   $??18,1,0,0q?6<   $	??Top=?6N   $??12(?6f   $??19??6t   $??26??6?   $??45??6?   $??2??6?   $??1??6?   ?? 5I   
   -   5        
 ??Name$
 Add_Btn??5I      $	??Add??6"   
??? ?|?$??23??6]   $??2??6k   $??1??6   ?? 5J   
   -   5        $??
Cancel_Btn??5J      $??Cancel??6#   
??? ?|?$??23??6d   $??4??6r   5L      5M      