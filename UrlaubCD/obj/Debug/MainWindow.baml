   M S B A M L   `   `   `  ����C  ?UrlaubCD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UrlaubCDUrlaubCD  5   	   A%clr-namespace:UrlaubCD.WPFUserControlUrlaubCD.WPFUserControl  5   	       UrlaubCD.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $lclr-namespace:UrlaubCD     5   	   9control%clr-namespace:UrlaubCD.WPFUserControl     5   	     � Title$  	UrlaubCDs��5	   	   $	��600��6   $	��800��6(   .��5      �  � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $	��50* �6   �� 5      $	��10* �6    � ColumnDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��50* �6   �� 5      $	��10* �6   .+�5   
   �� -   5         ��Name$	 txb��5      $��0��6   $��1��6,   $
��Auto=�6<   6n   ]� 5   
   $��1��6   $��0��6!   
���iii�.+�5      ]� $	��Top=�6   .+�5      '   !UrlaubCD.WPFUserControl.SearchBox  $
��Left=�6$   $��	20,5,10,5q�6:   6O   5      �� 5      $	��Top=�6   �� 5!      $	��Top=�6   .��6)   	PLAYLISTS64   �� 5#      $	��Top=�6   .��5%      �� -   5         ��Name$ PlaylistStackPanel��5%      5)      5+      5/      ]� 52   
   $��1��6   $��1��6!   
�������.+�54      ]� $	��Top=�6   $��30��6-   .+�55        $��Right=�6$   $��
200,5,10,5q�6;   6Q   56      �� 57      $	��Top=�6   �� 59      $	��Top=�6   .��6)   SONGS60   �� 5:      $	��Top=�6   .��5<      �� -   5        $ songsStackPanel��5<      5@      5B      5D      �� 5G   
   -   5         ��Name$ 
testButton��5G      $��2��62   $��2��6?   $��10q�6O   .��6[   TEST6a   5J      5L      