   M S B A M L   `   `   `  ����@  <MNAIS, Version=1.0.2.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MNAISMNAIS  5      -clr-namespace:MNAIS.UtilityMNAIS.Utility  5          MNAIS.Adminstration   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:MNAIS     5      /utilityclr-namespace:MNAIS.Utility     5      $
��Auto��5      $
��Auto��6     � 	Resources  5
      !   MNAIS.Utility.BoolConverter   BoolConverter��%   &	          5   
   6    5      .��5      �� $
��Auto=�6   $
��Auto=�68   .��5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   $��0��6$   $��5,5,5,5q�64   ��5      �� 5      $��Administration Options��6    $
��Bold"�6>   .��5"      �  5$      �� 5%      $
��Auto �6(   68   �� 5&      $
��Auto �6(   68   �� 5'      $
��Auto �6(   68    5*      �� 5+      $
��Auto �6+   6:   �� 5,      $��* �6+   67   .+�5/      �� $��0��6    $��0��6-   $��5,5,5,5q�6>   $��Product License Status :��6O   �� 50      $��5,5,5,5q�6    $��0��61   $��1��6>   ��6N   �� *LicenseStatus+y�6m   �� �� 52      $��1��6    $��0��6-   $��5,5,5,5q�6>   $��Product Language :��6O   6l   �� 54      -   5        $��
c_Language��54   ]   $��1��6   $��1��6,   $��5,5,5,5q�6=   $	��100��6N   
����ح�
������  �� DisplayMemberPath$ Description��56   %    �� SelectedValuePath$ Value��6E   ��55      ��  �� Source     MNAIS.EnumerationExtension  *M� *local:Language++ �� SelectedValue 58      �� 59       �� Path$ SelectedLanguage�6&    �� UpdateSourceTrigger$ PropertyChanged=�6>   	 �� Mode$	 TwoWay=�6d   5<      �� 5>      $��5,5,5,5q�6    $��2��61   $��0��6>   $��Product Log Location :��6N   $
��Left=�6l   $
��Auto��6�   $
��Auto��6�   6�   � 5@      $��2��6   $��1��6(   {�68   �� *IsLogFileLocationEnabled+ 5B      �� 5C      $��* �6/   �� 5D      $
��Auto �6/   .+�5G      �� -   5        $��	c_LogFile��5G   "   $��5,5,5,5q�63   $��0��6D   $��Stretch=�6T   $
��Auto��6r   $
��Auto��6�   $	��220��6�   
 �� 
IsReadOnly
 . ��5H      �� *LogFilePath+$ PropertyChanged=�6e   �� 5J      
����ح�
������ $��1��6R   $��0,5,5,5q�6b   $��Browse��6s   $��80��5K   F   $
��Auto��6Q   $��Right=�6_   ��6   �� *LogFileBrowseCommand+5M      5O      5Q      5S      �� 5U      $��1��6   $��0��6$   $��5,5,5,5q�64   ��5V      �� 5W      $��Result Options��6    $
��Bold"�66   .��5Z      �  5\      �� 5]      $
��Auto �6(   68   �� 5^      $
��Auto �6(   68   �� 5_      $
��Auto �6(   68    5b      �� 5c      $
��Auto �6+   6:   �� 5d      $��* �6+   67   .+�5g      �� $��0��6    $��0��6-   $��5,5,5,5q�6>   $��Precision (in decimals):��6O   �� 5h       � ToolTip$. (Enter no of digits to see after decimal.��6   $��5,5,5,5q�6Q   $��0��6b   $��1��6o   ��6   �� *SelectedPrecision+�� 5j      $��1��6    $��0��6-   $��5,5,5,5q�6>   $��Display as  :��6O   6g   �� 5k      $$ Choose how to display results.��6   $��1��6H   $��1��6U   $��5,5,5,5q�6f   
����ح�
������ $ Description��5m      $ Value��6;   ��5l      ��    *M� *local:ResultsDisplay++ 5n      �� 5o      $" SelectedResultsDisplayOption�6&   $ PropertyChanged=�6J   $	 TwoWay=�6p   5q      5s      5u      �� 5w      $��2��6   $��0��6$   $��5,5,5,5q�64   ��5x      �� 5y      $��Debugging Options��6    $
��Bold"�69   .��5|      �  5~      �� 5      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68    5�      �� 5�      $
��Auto �6+   6:   �� 5�      $��* �6+   67   .+�5�      �� $��0��6    $��0��6-   $��5,5,5,5q�6>   $��Generate Log Files :��6O   �� 5�      $* $Choose option to generate log files.��6!   $��5,5,5,5q�6P   $��0��6a   $��1��6n    �� Orientation$ 
Horizontal=�6~   $��Stretch=�6�   .+�5�      �� $	��Yes��6#   $��5,5,5,5q�61    d� 	IsChecked 6B   �� *GenerateLogFile+�� 5�      $��No��6#   $��35,5,5,5q�60    6B   �� *GenerateLogFile+ �� 	Converter# [  5�      �� 5�      $��1��6    $��0��6-   $��5,5,5,5q�6>   $��Show Messages :��6O   6i   �� 5�      $% Choose option to show messages.��6!   $��5,5,5,5q�6K   $��1��6\   $��1��6i   $ 
Horizontal=�6y   $��Stretch=�6�   .+�5�      �� $	��Yes��6#   $��5,5,5,5q�61    6B   �� *ShowMessages+�� 5�      $��No��6#   $��35,5,5,5q�60    6B   �� *ShowMessages+# [  5�      5�      5�      5�      5�      5�      