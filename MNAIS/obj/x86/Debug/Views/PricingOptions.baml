   M S B A M L   `   `   `  ����@  <MNAIS, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MNAIS.UtilityMNAIS.Utility  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      clr-namespace:MNAISMNAIS  5           MNAIS.Views.PricingOptions   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /utilityclr-namespace:MNAIS.Utility     5      6sys&clr-namespace:System;assembly=mscorlib     5      %localclr-namespace:MNAIS     5      $
��Auto��5	      $
��Auto��6     � 	Resources  5      '   !MNAIS.Utility.PercentageConverter   PercentConverter �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value �� 
TargetName	 �� Triggers
 �� RelativeSource �� Path �� Binding �� Value j� Stretch !� Visual��%s  &	        '_�      '�� j      5   
   6&   �� 5   
   " _�6)   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       �@_�HasDropShadow . 6.   �� 5       � �� 5      �� 5      " _�6&   .R�5      �� $��Border��6!   
�������
�������$��1q�5      #��z��5      #��z��5      ._�5      �� $��4q�6/   $
��Left=�6:   $	��Top=�6U   5       5      P� 5       �@_�HasDropShadow . . �5   "   �� $ Border��6)    �@��CornerRadius$ 4��6U   �� 5    "   $ Border��6)    �@J�SnapsToDevicePixels . 6\   5!      5#      5%      5&      �� 5(   
   " ��6   .�5*      ��  �@=�ErrorTemplate 5+      �� 5,      .R�5-      �� 
���  ��$��2q�63   ._�5.      �� $��	MyAdorner��68   5/      50      52      	 54      P� 56       �@=�HasError . . �57      ��  �@�ToolTip 60   �� 
 �� *Self+$) #(Validation.Errors)[0].ErrorContent�59      �� 5;       6   �� 
 �� *Self+$
 Text�  6b   .a�5<      ��  � �� 5=      !� 5>      $
 None=�6*    5?   "   �� 5@   &   $��Enter value��60   
�������6C   5B      5D      5E      �� 5G       6   �� 
 �� *Self+$
 Text� 6b   Z� .a�5H      ��  � �� 5I      !� 5J      $
 None=�6*    5K   "   �� 5L   &   $��Enter value��60   
�������6C   5N      5P      5Q      �� 5S       6   �� 
 �� *Self+$ 	IsFocused� True6g   .a�5T      ��  � �� 5U      !� 5V      $
 None=�6*   5W      5Y      5Z      5^      5`      .��5b      �� 
���   �$��1q�6!   ._�5d   
   �� $
��Auto=�6   $
��Auto=�6<   .��5f      �  � RowDefinitions 5h      �� 5i      $
��Auto �6$   �� 5j      $
��Auto �6$   �� 5k      $
��Auto �6$   .+�5n      �� $��5,5,5,5q�6   $��0��6,   ��5o      �� 5p      $��Pricing Options - I��6$   $
��Bold"�6?   .��5s      �  5u      �� 5v      $
��Auto �6,   �� 5w      $
��Auto �6,   �� 5x      $
��Auto �6,    � ColumnDefinitions 5{      �� 5|      $
��Auto �6/   �� 5}      $
��Auto �6/   �� 5~      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   .+�5�      �� $��5,5,5,5q�6$   $��Optimal No of years :��65   $��0��6R   $��0��6_   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��1��6<   $	��100��6L   $	��200��6[   ��5�      �� *OptimalNoOfYears+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� Empty �� TargetNullValue# Z 5�   [   �� 5�      $��5,5,5,5q�6$   $��Confidence Level (%) :��65   $��0��6S   $��2��6`   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��3��6<   $	��100��6L   $	��200��6[   ��5�      �� *ConfidenceLevel+$ PropertyChanged=� .  �� 	Converter# [  5�   [   �� 5�      $��5,5,5,5q�6$   $��X Stdev (%) :��65   $��0��6J   $��4��6W   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��5��6<   $	��100��6L   $	��200��6[   ��5�      �� *StandardDeviation+$ PropertyChanged=� . # [  5�   [   �� 5�      $��5,5,5,5q�6$   $!��Data Uncertainity Loading :��65   $��1��6X   $��0��6e   �� 5�      $	��100��6q    �� DisplayMemberPath$ Description��5�       �� SelectedValuePath$ Value��69   
����ح�
������ $��5,5,5,5q�5�      $��1��6*   $��1��67   ��5�   #   ��  �� Source     MNAIS.EnumerationExtension  *M� *local:DataLoading++ �� SelectedValue 6�   �� *!SelectedDataUncertainityLoading+5�   7   �� 5�      $��5,5,5,5q�6$   $��Uncertainity Value (%) :��65   $��1��6U   $��2��6b   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��1��6/   $��3��6<   $	��100��6L   $	��200��6[   {�5�   :   �� *IsUncertainityEnabled+��5�      �� *UncertainityValue+$ PropertyChanged=� . # [  5�   [   �� 5�      $��5,5,5,5q�6$   $��PML Return Period :��65   $��2��6P   $��0��6]   �� 5�      $	��100��6s   $ Description��5�      $ Value��69   
����ح�
������ $��5,5,5,5q�5�      $��2��6*   $��1��67   ��5�   #   ��    *M� *local:HistoricalPML++ 6�   �� *SelectedHistoricalPML+5�   7   �� 5�      $��5,5,5,5q�6$   $ ��Return Period (In Years) :��65   $��2��6W   $��2��6d   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��2��6/   $��3��6<   $	��100��6L   $	��200��6[   ��5�      �� *ReturnPeriod+$ PropertyChanged=� . # Z 5�   [   5�      5�      �� 5�      $��5,5,5,5q�6   $��1��6,   ��5�      �� 5�      $��Pricing Options - II��6$   $
��Bold"�6@   .��5�      �  5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,    5�      �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   .+�5�      �� $��5,5,5,5q�6$   $��A O Expenses (%) :��65   $��0��6O   $��0��6\   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��1��6<   $	��100��6L   $	��200��6[   ��5�      �� *
AOExpenses+$ PropertyChanged=� . # [  5�   [   �� 5�      $��5,5,5,5q�6$   $��Service Tax (%) :��65   $��0��6N   $��2��6[   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��3��6<   $	��100��6L   $	��200��6[   ��5�      �� *
ServiceTax+$ PropertyChanged=� . # [  5�   [   �� 5�      $��5,5,5,5q�6$   $��Profit Margin (%) :��65   $��0��6P   $��4��6]   �� 5�      -   5        
����ح�$��5,5,5,5q�5�      $��0��6/   $��5��6<   $	��100��6L   $	��200��6[   ��5�      �� *ProfitMargin+$ PropertyChanged=� . # [  5�   [   �� 5�      $��5,5,5,5q�6$   $��Return on Capital :��65   $��1��6P   $��0��6]   �� 5�      $	��100��6u   $ Description��5�      $ Value��69   
����ح�
������ $��5,5,5,5q�5�      $��1��6*   $��1��67   ��5�   #   ��    *M� *local:ReturnOnCapital++ 6�   �� *SelectedReturnOnCapital+5�   7   �� 5�      $��5,5,5,5q�6$   $#��Return on Capital Value (%) :��65   $��1��6Z   $��2��6g   �� 5�      -	   5        
����ح�$��5,5,5,5q�5�      $��1��6/   $��3��6<   $	��100��6L   $	��200��6[   ��5�      �� *ReturnOnCaptial+$ PropertyChanged=� . # [  {�6�   �� *IsReturnOnCapitalEnabled+5�   [   �� 5�      $��5,5,5,5q�6$   $��Franchise / Deductible :��65   $��2��6U   $��0��6b   �� 5�      $	��100��6o   $ Description��5�      $ Value��69   
����ح�
������ $��5,5,5,5q�5�      $��2��6*   $��1��67   ��5�   #   ��    *M� *local:Franchise++ 6~   �� *SelectedFranchise+5�   7   �� 5�      $��5,5,5,5q�6$   $(��"Franchise / Deductible Value (%) :��65   $��2��6_   $��2��6l   �� 5�      -
   5        
����ح�$��5,5,5,5q�5�      $��2��6/   $��3��6<   $	��100��6L   $	��200��6[   ��5�      �� *	Franchise+$ PropertyChanged=� . # [  5�   [   5�      5�      �� 5�      $��5,5,5,5q�6   $��2��6,   ��5�      �� 5�      $��Generate Results��6$   $
��Bold"�6<   .��5�      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5�      ��  � ToolTip$- 'Enabled when all the values are filled.��6l    ^� ShowOnDisabled . $��5,5,5,5q�5�   !   $��Run Analytics��62   $	��250��6J   
����ح�
������ {�5�   !   �� *IsRunAnalyticsEnabled+��6M   �� *
RunCommand+5�   n   5�      5�      5�      5�      5     5     