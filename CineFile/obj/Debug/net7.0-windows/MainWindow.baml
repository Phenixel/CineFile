   M S B A M L   `   `   `  ����C  ?CineFile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CineFileCineFile  5   	       CineFile.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:CineFile     5   	     � Title$  CineFile��5	   	   $	��450��6   $	��800��6'    � DataContext 5   	   ��  �� RelativeSource �� *Self+ � 	Resources 5
      &    CineFile.InverseBooleanConverter   InverseBooleanConverter   BooleanToVisibilityConverter �� 
TargetType �� Triggers �� Binding �� Value �� Property	 �� Value��%�  &	        &	      '�� &       5   
   6(   �� 5   
   6'   �� 5   
   " ��6    5      �� 5       6   �� *
IsSelected+ True6=   .a�5      ��  � ��
	 ��ح�63   �� 5       � ��
	 �   �63   5      �� 5       6   �� *
IsSelected+	 False6=   .a�5      ��  � ��
	 ���� 63   �� 5       � ��
	 �   �63   5      5      5      .��5      � 
 � RowDefinitions
 5   
   �� 5       $
��Auto �6   �� 5!      $��* �6   .+�5%   
   �� 
������� �� Padding$ 10q�6(   ._�5&      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   .+�5'      �� $��CineFile��6   $��20(�6,   �� 5(      ��6   �� *
Categories+��5)      �� 5*      .R�5+      �� $ 
Horizontal=�6)   5,      ��5.      �� 5/      .R�50      �� $��5q�53   %   ��50   %   �� *Nom+��51   %   �� *,*DataContext.ToggleCategorySelectionCommand+ ��  �� AncestorType
 Window��52   %   �� {�54   %   �� *
IsSelected+ �� 	Converter# [ 55      57      o� 58      .@�59      m� $��_Action��6   .@�5:      m� -   5        $��	_Add Film��5:   #   66   m� 5;      $��
_Edit Film��6#    m� Command 67   �� *EditFilmCommand+m� 5<      $��_Delete Film��6#    69   �� *DeleteFilmCommand+5=      5>      5?      5@      �� 5C   
   $��1��6   .��5D      �� ��6   �� *Films+��5E      �� 5F      .R�5G      �� 
���   �$��1q�65   $��5q�6I   $	��200��6T   ._�5H      �� .+�5I   "   �� $	��200��6E   $	��300��6Q   $��UniformToFill=�6^   ��6(   �� *	LienImage+6^   �� 5J   "   $��Center=�6C   $
��Bold"�6`   ��6,   �� *Titre+6`   �� 5K   "   $��Center=�6I   ��6,   �� *Realisateur+6I   �� 5L   "   $��Center=�6I   ��6,   �� *AnneeSortie+6I   5M      5N      5O      ��5Q      �� 5R      .R�5S      �  � Orientation$ 
Horizontal=�6$   5T      5V      5W      5X      5Y      