   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5   	   @ <MNAIS, Version=1.0.2.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MNAISMNAIS 5   	   )clr-namespace:MNAIS.ViewsMNAIS.Views 5   	      MNAIS.MNAISWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7Fluent$clr-namespace:Fluent;assembly=Fluent     5   	   %localclr-namespace:MNAIS     5   	   *viewclr-namespace:MNAIS.Views     5   	     � Title$=  7modified National Agricultural Insurance Scheme (mNAIS)��5   	   $
��Auto��5   	   $
��Auto��6   $	��600��6$   $	��600��63   
������� � WindowStartupLocation$ CenterScreen=�5	   	    � Icon$' !/MNAIS;component/Images/MNAIS.png��6.   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
      Fluent.Ribbon  -   5          Name$ Ribbon��5         AutomaticStateManagement . $��0��6H   $��2��6U      Tabs. 5         Fluent.RibbonTabItem  -   5        $��
tbContract��5   #    � ToolTip$) #Contract and Yield Data Management.��65   	   Header	 5      �� 5      -   5        $��Contract��5   V   ��5      �� 5      .�5    "   �� 
 �� Property
 � �� �� Value
 �   �6I   �� 5!   "   
 � ��$
 Bold"�6I   5"      5$         Groups. 5'         Fluent.RibbonGroupBox  .@�5)      ��  �� Orientation$ 
Horizontal=�6!   .+�5*         Fluent.Button  -   5        $��5,0,5,0q�5*   N      Header$ Create Contract��6_      	LargeIcon$ Images\Add.png��6�      Icon$ Images\Add.png��6�   {�6(   �� *IsCreateEnabled+6�     5+      $��5,0,5,0q�6N   $ Delete Contract��6�   $ Images\delete.png��6�   $ Images\delete.png��6�   {�6(   �� *IsDeleteEnabled+��6_   �� *DeleteContractCommand+6�     5-      -   5        $��5,0,5,0q�5-   (   $ Import Contract��69   $ Images\import.png��6q   $ Images\import.png��6�   6�     5.      -   5        $��5,0,5,0q�5.   (   $ Export Contract��6c   $ Images\export.png��6�   $ Images\export.png��6�   {�69   �� *IsDataImportEnabled+6�   5/      51        53      .@�55        $��5,0,5,0q�6$   $ 	Crop Data��56   $   $ Images\csv.png��67   $ Images\csv.png��6R   {�55   5   �� *IsDataImportEnabled+��6_   ��  �� Path$" ImportedYieldDataLoadCommand�56   j   58      5:        5<      -   5        $��
tbAnalysis��5<   R   $" Analysis Options Management.��6d    ^� ShowOnDisabled . {�6#   �� *IsAnalysisOptionsEnabled+	 5>      �� 5?      -   5        $��Analysis Options��5?   V   ��5@      �� 5A      .�5B   "   �� 
 � ��
 �   �6I   �� 5C   "   
 � ��$
 Bold"�6I   5D      5F      . 5I        .@�5K      �� $ 
Horizontal=�6!   .+�5M        $��5,0,5,0q�6(   $ Modelling Options��5N   (   $ Images\modelling.png��6C   $ Images\modelling.png��6d   ��5M   9   �� $! ModellingOptionsLoadCommand�5N   �     5P      $��5,0,5,0q�6(   $ Pricing Options��5Q   (   $ Images\pricing.png��6A   $ Images\pricing.png��6`   ��5P   9   �� $ PricingOptionsLoadCommand�5Q   |   5R      5T      5V        5X      -	   5        $��	tbResults��5X   J   $ Results Management.��6[    . {�6#   �� *IsResultsEnabled+	 5Y      �� 5Z      -
   5        $��Results��5Z   T   ��5[      �� 5\      .�5]   "   �� 
 � ��
 �   �6I   �� 5^   "   
 � ��$
 Bold"�6I   5_      5a      . 5d        $��5,0,5,0q�6(   .@�5f      �� $ 
Horizontal=�6!   .+�5h        $��5,0,5,0q�6(   $ Results��69   $ Images\results.jpg��5i   (   $ Images\results.jpg��6G   ��5h   J   �� $ ResultSummaryLoadCommand�5i   c     5k      $��5,0,5,0q�6(   $ Premium Charts��69   $ Images\charts.png��5l   (   $ Images\charts.png��6F   ��5k   Q   �� $ PremiumChartsLoadCommand�5l   a     5n      $��5,0,5,0q�6(   $ Yield Graphs��69   $ Images\graphs.png��5o   (   $ Images\graphs.png��6F   ��5n   O   �� $ YieldGraphsLoadCommand�5o   a   5q      5s      5u        5w      -   5        $��tbAdministration��5w   #   	 5x      �� 5y      -   5        $��Administration��5y   S   ��5z      �� 5{      .�5|   "   �� 
 � ��
 �   �6I   �� 5}   "   
 � ��$
 Bold"�6I   5~      5�      . 5�        $��5,0,5,0q�6(   .@�5�      �� $ 
Horizontal=�6!   .+�5�        $��5,0,5,0q�6(   $ Settings��69   $ Images\settings.png��5�   (   $ Images\settings.png��6H   ��5�   K   �� $ AdministrationLoadCommand�5�   e   5�      5�      5�         QuickAccessItems 5�          Fluent.QuickAccessMenuItem  5�      $
��Help��6-    m� Command 6;   �� *HelpLoadCommand+  5�      $��About��6-    6<   �� *AboutLoadCommand+  5�      $
��Exit��6-    6;   �� *ExitCommand+   ToolBarItems 5�        5�         Size$ Small=�6    $ Click to see Help��6-   $ Images/Help.png��6m   ��6I   �� *HelpLoadCommand+6m     5�      $ Small=�6    $ Click to see About��6-   $ Images/About.png��6o   ��6J   �� *AboutLoadCommand+6o   5�      �� 5�   
   -   5        $��c_contentControl��5�      $
��Auto��61   $
��Auto��6>   |�.  $��1��6^   $��1��6k   ��.  ��5�      �� *PresenterContent+6R   �� 5�   
   -   5        $��c_projectControl��5�   K   $
��Auto=�6   $
��Auto��6c   $
��Auto��6p   
������ |�.  $��1��5�      $��0��6,   ��.  ��6<   �� *ProjectInfoPresenter+6y   �� 5�   
   
����ح�$��2��6+   $��2��68   .@�5�      �� $	��400��6   $��Stretch=�6(   ��6F   �� *	Message+ �� UpdateSourceTrigger$ PropertyChanged=���5�      �� 5�       �� 
TargetType" ��6   .�5�      �� 
 � ��
 �   �67    �� Triggers 5�      �� 5�       �� Binding 6*   �� *	ErrType+ �� Value	 Debug6F   .a�5�   "   �� 
 � ��
 � � �6?   5�      �� 5�       6*   �� *	ErrType+ Warning6F   .a�5�   "   �� 
 � ��
 ��iA�6?   5�      �� 5�       6*   �� *	ErrType+	 Error6F   .a�5�   "   �� 
 � ��
 �  ��6?   5�      5�      5�      5�      5�      5�      