   M S B A M L   `   `   `  ����@  <MNAIS, Version=1.0.2.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MNAISMNAIS  5   	        MNAIS.Views.LicenseRequest   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   %localclr-namespace:MNAIS     5   	     � Title$  MNAIS License Manager��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � SizeToContent$ WidthAndHeight=�5   	    � Icon$ ..\Images\MNAIS.png��5   	   $	��500��5	   	   $
��Auto��6   $
��Auto��6&    � WindowStyle$ ThreeDBorderWindow=�5
   	    � 
ResizeMode$ NoResize=�6*   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��5,5,5,5q�6    ��5      �� 5      $��Choose Option��6   $
��Bold"�61   .��5      �  5      �� 5      $
��Auto �6$    � ColumnDefinitions 5       �� 5!      $
��Auto �6'   �� 5"      $��* �6'   .+�5%      �� $��5,5,5,5q�6   $��License Status :��6-   $��0��6E   $��0��6R   �� 5'      -   5        $��cmbLicenseStatus��5'   ,   $��5,5,5,5q�6   $	��150��6D   $
��Auto��6S   $��0��6`   $��1��6m   
����ح�
������  �� DisplayMemberPath$ Description��5*      	 �� SelectedValuePath$	 Value��6;   ��5(      �� 
 �� Source
     MNAIS.EnumerationExtension  *M� *local:LicenseFile++ �� SelectedValue 5)      �� *SelectedLicenseOption+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5,      5.      50      9� 52   
   -   5        $��expUpdateLicense��52   1   $��1��6   $��5,5,5,5q�6    ��53      �� 54      $��Update License��6   $
��Bold"�62   .��57      �  59      �� 5:      $
��Auto �6$   �� 5;      $
��Auto �6$   �� 5<      $
��Auto �6$    5?      �� 5@      $
��Auto �6'   �� 5A      $��* �6'   .+�5D      �� $��5,5,5,5q�6   $��License File :��6-   $��0��6C   $��0��6P   � 5F      $��0��6   $��1��6$    5H      �� 5I      $��* �6+   �� 5J      $
��Auto �6+   .+�5M      �� -   5        $��txtLicenseFile��5M      $��5,5,5,5q�64   $��0��6E   $��Stretch=�6U   $
��Auto��6s   $
��Auto��6�   $	��220��6�    �� 
IsReadOnly . 6�   �� 5N      -   5        
����ح�
������ $	��100��5N   j   $��1��6y   $��5,5,5,5q�6�   $��Browse��6�   5P      �� 5R      $��5,5,5,5q�6   $��System File :��6-   $��1��6B   $��0��6O   � 5T      $��1��6   $��1��6$    5V      �� 5W      $��* �6+   �� 5X      $
��Auto �6+   .+�5[      �� -   5        $��txtPublicKeyFile��5[      $��5,5,5,5q�66   $��0��6G   $��Stretch=�6W   $
��Auto��6u   $
��Auto��6�   $	��220��6�    . 6�   �� 5\      -   5        
����ح�
������ $	��100��5\   l   $��1��6{   $��5,5,5,5q�6�   $��Browse��6�   5^      �� 5`      -   5        $��2��5`      $��2��6&   $ ��Update License Information��6:   $	��200��6{   
����ح�
������ $��Center=�5b      $��5,5,5,5q�66   5d      5f      9� 5h   
   -   5        $��expRequestLicense��5h   1   $��2��6   $��5,5,5,5q�6    ��5i      �� 5j      $��Request License��6   $
��Bold"�63   .��5m      �  5n      �� 5o      $
��Auto �6$   �� 5p      $
��Auto �6$    5s      �� 5t      $
��Auto �6'   �� 5u      $��* �6'   .+�5w      �� $��0��6   $��0��6)   $��5,5,5,5q�69   $��Select User :��6J   �� 5x      -	   5        $��cmbUserName��5x   I   $��0��6   $��1��6(   $��5,5,5,5q�68   
����ح�
������ 6t   �� 5z      -
   5        $��1��5z      $��2��6&   $��Generate Request File��6:   
����ح�
������ $	��200��6�   $��Center=�5{   ;   $��5,5,5,5q�6X   5}      5      �� 5�   
   $��3��6   $��5,5,5,5q�6    ��5�      �� 5�      $��Terms And Conditions��6   $
��Bold"�68   .��5�      ��  �� MaxLines$ 5��6   $	��500��6#    .  �� TextWrapping$ WrapWithOverflow=�5�      " �� VerticalScrollBarVisibility$ Visible=�61   $����Warning : This computer program is protected by copyright law and international treaties. Unauthorized reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal penalties, and will be prosecuted to the maximum extent possible under the law.                                                                  (C) Asia Risk Centre 2013��5�      5�      5�      5�      