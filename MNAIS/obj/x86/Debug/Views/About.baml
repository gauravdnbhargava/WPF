   M S B A M L   `   `   `  ����	  MNAIS    MNAIS.Views.About   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowStartupLocation$  CenterOwner=�5   	    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � SizeToContent$ Height=�5   	    � Title$ About��5	   	   $
��Auto��6   $	��450��6%    � 	Resources 5         DisplayAreaStyle �� 
TargetType �� Property	 �� Value  BackgroundStyle
 �� EndPoint � Offset � Color  ParagraphSeparator  LinkLabelStyle  ReadOnlyDescStyle  OkButtonStyle��%�  &	        &	 P     '�� �    &	 �    &	     &	 �    &	 �    �� 5   
   " ��6)   .�5      ��  � ��$	 
10,10,10,5q�6'   5      �� 5   
   " ��6(   .�5      ��  � ��	 5      �� 5      $	
 0,1&�6*   .��5      � $ 0Q�6'   $ 	LightBlue��62   � 5      $
 0.55Q�6'   $ White��65   � 5      $
 0.85Q�6'   $ White��65   � 5      $ 1Q�6'   $ 	LightBlue��62   5      5      5      �� 5   
   " ��6   .�5      ��  � ��$	 0q�6(   5      �� 5   
   " ��6+   .�5      ��  � ��$	 0,10,0,0q�6(   5       �� 5!   
   .�5"      ��  � ��$	 0q�60   �� 5#       � ��$	 Center=�6:   5$      �� 5%   
   " ��6*   .�5&      ��  �@��MinLines$	 6��6)   �� 5'       �@��MaxLines$	 6��6)   �� 5(       �@��
IsReadOnly	 . 6+   �� 5)       �@��TextWrapping$	 WrapWithOverflow=�6-   �� 5*      # �@��VerticalScrollBarVisibility$	 Visible=�6<   5+      �� 5,   
   " ��6&   .�5-      ��  � ��$	 75��6)   �� 5.       � ��$		 0,5q�6'   �� 5/       � ��$	 Right=�6/   �� 50       �@��	IsDefault	 . 6*   �� 51       �@��IsCancel	 . 6)   52      53      .��55      �� #��[ 6   .+�56   
   �� $,��&/MNAIS;component/Images/AboutMNAIS.jpg��6   �� 57   
   #��[  6    � DataContext 58      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" � �� Mode$ OneTime=�.+�59      �� -   5         ��Name$ productName��59      ��5:      �� $ OneTime=� �� Path$ Product��� 5;       �� Orientation$ 
Horizontal=�6   .+�5<      �� -   5        $ versionLabel��5<      VersionLabel��$��
Version - ��6C   �� 5=      -   5        $ version��5=      version����69   �� $ OneTime=�$ Version�5>      �� 5?      -   5        $ 	copyright��5?      ��6(   �� $ OneTime=�$ 	Copyright��� 5@      -   5        $ company��5@      ��6%   �� $ OneTime=�$ Company��� 5A      -   5        $ reserved��5A      $��All Rights Reserved.��6'   �� 5B      -   5        $
 info��5B      #��[ 6#   $��Product details:��5C      �� 5D      -   5         ��Name$ description��5D      #��[ 5E      ��5D   +   �� $ OneTime=�$ Description�5E      ]� 5F      .+�5G      �� -	   5         ��Name$ okButton��5G      #��[ 6+   $��OK��5H      �� 5I      -
   5        $ productLink��5I      #��[ 6-   .��5J      �� -   5         ��Name$ 	hyperlink��5J       	hyperlink��#��[ 6y   ��6E   �� $ OneTime=�$ LinkUri�.�5K   V   .�5L      �� ��6$   �� $ OneTime=�$ LinkText�5M      5N      5O      5P      5Q      5R      