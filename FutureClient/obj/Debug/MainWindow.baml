   M S B A M L   `   `   `  ����G  CFutureClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FutureClientFutureClient  5   	   ?$clr-namespace:FutureClient.ConverterFutureClient.Converter  5   	   K*clr-namespace:FutureClient.ValidationRulesFutureClient.ValidationRules  5
   	       FutureClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FutureClient     5   	   :	converter$clr-namespace:FutureClient.Converter     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   @validationrules*clr-namespace:FutureClient.ValidationRules  5
   	   -   5           MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   $	��800��5   %   $
��1200��62   $	��800��6?   $
��1200��6O    � Icon$ icon.ico��6_    � Title 6	   �� *	MainTitle+ � 	Resources 5      0   *FutureClient.Converter.FutureNameConverter  fnConverter/   )FutureClient.Converter.DirectionConverter  directionConverter+   %FutureClient.Converter.ValueConverter  valueConverter  AccountView �� Source  FinishedView  ToDealsView 	 	PairsView  
 TabControlWithUnderLineStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 �Name J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation   TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name ��Name K� IsItemsHost   TabIndex K� ZIndex	 �� X1	 �� X2 �� RelativeSource ��Name   DirectionalNavigation ��Name �� SelectedContentTemplate �� SelectedContent" �� SelectedContentStringFormat �� ContentStringFormat �� Triggers  P� Property! P� Value" �� 
TargetName  TabItemExWithUnderLineStyle# ��Name$ � ToolTip% ^� 
Conditions& �� Binding' �� Value( �� AncestorLevel) �� AncestorType  TabItemStyle* �� Triggers��%�  &	       &	      &	 &     &	 9     &	 ~     &	 �     &		     &	
 M    &	 �	    &	 :      5   
   6)     5   
   6(     5   
   6$   �� 5   
   -   5         5   3   �� *AccountIpList+6V   �� 5   
   -   5         5   4   �� *ExpiredlatestIpList+6]   �� 5   
   -   5         5   3   �� *LatestIpList+6U   �� 5   
   -   5         5   1   �� *
PairList+6O   �� 5   
   " ��65   .�5      ��  � ��$ 2q�6(   �� 5        � ��$ Center=�6;   �� 5!       � ��$ Center=�69   �� 5"       � ��
 �����6+   �� 5#       � ��
 �����6,   �� 5$       � ��$ 1q�60   �� 5%       � ��# � ��6+   �� 5&       � �� 5'      �� 5(      "	 ��6&   .R�5)      � $
 templateRoot��6   }�. #��z��6I    . $ Local=�6�    5*      �� 5+   "   $ ColumnDefinition0��63   �� 5,   "   $ ColumnDefinition1��63   $��0 �6N    5.      �� 5/   "   $ RowDefinition0��60   $
��Auto �6H   �� 50   "   $ RowDefinition1��60   $��* �6H   .+�52      �� $ HeaderPanel��6'   $��Center=�6<   
������ $��0��6r    . $��0q�6�   $��0��6�   $ 1��6�   $ 1��6�   �� 53      $ 0��6#   
�������$	��0.1��6y   $��Bottom=�6�   $��0 0 0 1q�6�    .  6*   �� *ActualWidth+ �� *Self+6�   �� 54      $ ContentPanel��6%   #��z��6;   #��z��6g   #��z��6�   $��0��6�   $ 	Contained=�6�   $��1��6
  $ 2��6  $ Local=�67  ._�55   "   �� $ PART_SelectedContentHost��63   #��z 6U   #��z 6�   # z 6�   $��SelectedContent��6  $��0q�6!  # z 6,  56      57       58      P� 59        �@��TabStripPlacement$! Bottom=�6C   . �5:   "   �� $" HeaderPanel��6=    � ��$ 1��6V   �� 5;   "   $" ContentPanel��6=    � ��$ 0��6W   5<      P� 5=        �@��TabStripPlacement$
! Left=�6C   . �5>   "   �� $" HeaderPanel��6=    � ��$ 0��6V   �� 5?   "   $" ContentPanel��6=    � ��$ 0��6W   �� 5@   "   $" HeaderPanel��6@    � ��$ 0��6Y   �� 5A   "   $" ContentPanel��6@    � ��$ 1��6Z   5B      P� 5C        �@��TabStripPlacement$! Right=�6C   . �5D   "   �� $" HeaderPanel��6=    � ��$ 0��6V   �� 5E   "   $" ContentPanel��6=    � ��$ 0��6W   �� 5F   "   $" HeaderPanel��6@    � ��$ 1��6Y   �� 5G   "   $" ContentPanel��6@    � ��$ 0��6Z   5H      P� 5I        � {�! .  . �5J   "   �� $" templateRoot��6K   # � ��6e    � ��5K      5M      5O      5P      �� 5R   
   " ��65   .�5S      ��  � ��$ 30(�6)   �� 5T       � �� �#BEBEBE6+   �� 5U       � ��
 ���� 6+   �� 5V       � �� �#FFACAC6,   �� 5W       � ��$ 0q�6'   �� 5X       � ��$ Stretch=�6;   �� 5Y       � ��$ Stretch=�69   �� 5Z       � �� 5[      �� 5\      "	 ��6&   .R�5]      � $
 templateRoot��6    . 
������ .+�5^      �� $ 
_underline��6%   
�������$��0q�6M   #��z��6a   � 5_      .+�5`   "   �� $	# txt��6,   $y�Visible=�69   $��Center=�6N   $��Center=�6i   #��z��6�   #$ z��6�   #��z��6�   $��CharacterEllipsis=�6�   5a      5b       5c      P� 5d        �@��
IsSelected! . . �5e   "   ��  � ��$
 Bold"�6G   �� 5f   "    � ��
 �   �6G   5g      ^� 5i      % 5j   "   �� 5k   &   & 60   �� *IsMouseOver+ �� *Self+' true6v   �� 5l   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top6�   .0�5n   "   �� $	" txt��6?    � �� �#FFFFFF6P   5o      ^� 5p      % 5q   "   �� 5r   &   & 60   �� *	IsEnabled+ �� *Self+	' false6t   �� 5s   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Left6�   .0�5u   "   �� $" templateRoot��6<    �@J�Opacity$
 0.56Q�6V   5v      ^� 5w      % 5x   "   �� 5y   &   & 60   �� *	IsEnabled+ �� *Self+	' false6t   �� 5z   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��
' Bottom6�   .0�5|   "   �� $" templateRoot��6<    �@J�Opacity$
 0.56Q�6V   5}      ^� 5~      % 5   "   �� 5�   &   & 60   �� *	IsEnabled+ �� *Self+	' false6t   �� 5�   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��	' Right6�   .0�5�   "   �� $" templateRoot��6<    �@J�Opacity$
 0.56Q�6V   5�      ^� 5�      % 5�   "   �� 5�   &   & 60   �� *	IsEnabled+ �� *Self+	' false6t   �� 5�   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top6�   .0�5�   "   �� $" templateRoot��6<    �@J�Opacity$
 0.56Q�6V   5�      ^� 5�      % 5�   "   �� 5�   &   & 60   �� *
IsSelected+ �� *Self+' true6u   �� 5�   &   & 60   �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top6�   .0�5�   "   ��  �@K�ZIndex$ 1��6A   �� 5�   "   $	" txt��6?    � �� �#FFFFFF6P   �� 5�   "   $" 
_underline��6D    � ��$ 0 0 0 2q�6\   5�      5�      5�      5�      �� 5�   
   " ��6%   * 5�      P� 5�        �@��
IsSelected! .  . �5�      ��  � ��
 �����63   �� 5�       � ��$ Light"�6;   5�      P� 5�        �@��
IsSelected! . . �5�      ��  � �� �#632dc163   �� 5�       � ��$
 Bold"�6;   5�      P� 5�        �@J�IsMouseOver! . . �5�      ��  � �� �#632dc163   5�      .�5�      ��  � ��
 ���� 6+   �� 5�       � ��
 ���� 6,   �� 5�       � ��$ 22(�6)   5�      5�      .��5�      �  5�   
   �� 5�      $
��166* �6   �� 5�      $	��82* �6   �� 5�      $	��61* �6   �� 5�      $	��86* �6   �� 5�      $
��797* �6   .+�5�   
   �� -   5        #��[
 5�      ���	#FF632DC1
������ $��0q�6~   $��2��6�   .@�5�      �� -   5        $��DirectionItem��5�      $��88��6+   $��方向��66   $��50��6B   #��[ 6N   + � Cursor$
+ Hand��6�   .��5�      � 
�������.+�5�      �  5�      �� 5�      $	��10* �6,   �� 5�      $	��14* �6,   �� 5�      $��0* �6,   .+�5�      �� $��1q�6!   ��65   �� *TraderColor+._�5�      ]� $��2 10 0 0q�6(   .+�5�   "   � $��	5 10 5 10q�6'   $	��Top=�6:    5�   &   �� 5�   *   $��2* �68   �� 5�   *   $	��10* �68   �� 5�   *   $��2* �68   �� 5�   *   $	��10* �68    5�   &   �� 5�   *   $��6* �6;   �� 5�   *   $��6* �6;   �� 5�   *   $��6* �6;   �� 5�   *   $��6* �6;   .+�5�   &   ��  $  MaterialDesignSubtitle2TextBlock#��[ 60   $��自動開平��6j   $��0��6v   $��4q�6�   	� 5�   &   -   5        , 	�Name$, buyRadio��5�   H   - 	� 	GroupName$- 	TradeSide��62   $��1��6[   $��2��6h      MaterialDesignTabRadioButton#��[ 6|   ���#FF4500$��4q�6�   . d� 	IsChecked$
. TrueY�6�   $��买入��6�   6�   	� 5�   &   -	   5        $, 	sellRadio��5�   H   $- 	TradeSide��62   $��1��6[   $��2��6h   $��2��6x   #��[ 6�   ���#DCDCDC$��4q�6�   $. FalseY�6�   $��卖出��6�   6  �� 5�   &   #��[ 60   $��手動開平��6j   $��2��6v   $��4q�6�   	� 5�   &   -
   5        $, buyOpen��5�   H   $- 	TradeSide��62   $��0��6Y   $��3��6i   #��[ 6w   ���#DCDCDC$��4q�6�   $. FalseY�6�   $��买入开仓��6�   6�   	� 5�   &   -   5        $, buyClose��5�   H   $- 	TradeSide��62   $��1��6Z   $��3��6j   #��[ 6x   ���#DCDCDC$��4q�6�   $. FalseY�6�   $��买入平仓��6�   6�   	� 5�   &   -   5        $, sellOpen��5�   H   $- 	TradeSide��62   $��2��6Z   $��3��6j   #��[ 6x   ���#DCDCDC$��4q�6�   $. FalseY�6�   $��卖出开仓��6�   6�   	� 5�   &   -   5        $, 	sellClose��5�   H   $- 	TradeSide��62   $��3��6[   $��3��6k   #��[ 6y   ���#DCDCDC$��4q�6�   $. FalseY�6�   $��卖出平仓��6�   6�   5�   #   ]� 5�   "   $��5 10 5 0q�6,   $	��Top=�6>   .+�5�   &   �� $
��Left=�60   $��代碼:��6F   $��20(�6Q   
���   �$��0 0 25 0q�6r   $��Center=�6�   $��Center=�6�   �� 5�   &   -   5        / ��Name$/ codeCoboBox��5�   +   )  #MaterialDesignThemes.Wpf.HintAssist0   Hint$0 请选择证券代码��5�   /   1   HintOpacity$	1 .26Q�5�   +   
���   �2 w� TextPath$
2 Code��5�   +   3 �� 
IsEditable3 . ��5�   +   �� *
CodeList+4 �� SelectedItem4 5�   +   �� *SelectedCodeItem+��5�   *   �� 5�   .   .R�5�   2   $� 5�   /   ��5�   *   �� 5�   .   .R�5�   2   ]� ��.  .+�5�   6   �� $
��Left=�6A   $��15(�6m   ��6W   �� *Code+6m   �� 5�   6   $��Right=�6A   $��15(�6n   ��6X   �� *Name+6n   5�   3   5�   /   5�   '   5�   #   ]� 5�   "   $��5 10 5 0q�6,   $	��Top=�6>   .+�5�   &   �� $
��Left=�60   $��价格:��6F   $��20(�6Q   
���   �$��0 0 25 0q�6r   $��Center=�6�   $��Center=�6�   �� 5�   &   -   5        5 ��Name$5 
inputPrice��5�   .    % !MaterialDesignFloatingHintTextBox#��[ 6B   $��Center=�6}   .��6�   ��5   *   �� 5  .   6 �� Path$6 Price�67   7 �� UpdateSourceTrigger$7 PropertyChanged=�6E   5  /   .��5  8   5  '   5  #   ]� 5
  "   $��5 10 5 0q�6,   $	��Top=�6>   .+�5  &   �� $
��Left=�60   $��数量:��6F   $��20(�6Q   
���   �$��0 0 25 0q�6r   $��Center=�6�   $��Center=�6�   �� 5  &   -   5        $5 DirectionAmt��5  �   #��[ 6.   $��Center=�6i   .��6�   ��5  *   �� 5  .   $6 Amount�67   $7 PropertyChanged=�6F   8 �� ValidationRules8 5  2   3   -FutureClient.ValidationRules.NumberValidation  5  6   9   	ValidType$9 amount��6W   :   Ticks$: 1~�6j   
;   Min$; 1~�6t   
<   Max$< 50~�6|   5  /   .��5  8   5  '   5  #   �� 5  "   $��34��6-   �� 5  "   +  %MaterialDesignThemes.Wpf.ButtonAssist=   CornerRadius$= 10��6)   $��Bottom=�6z   ��6W   �� *TraderColor+��6�   �� *CommonNewOrderCommand+.��6�   提交訂單6�   5     5     �� 5     $��1��6#     MaterialDesignGroupBox#���  5  #   $��0q�5  #   > � UseLayoutRounding> .  . ��5  #   �� *ShowHQ.SCode+.��5      #	  MaterialDesignThemes.Wpf.Card	  +
  %MaterialDesignThemes.Wpf.ShadowAssist? 
  ShadowDepth$? Depth4=�62   @ 
  ShadowEdges$@ Bottom,Left=�6c   $��5q�6�   .��5!  "   � $��0,0,0,0q�6'    5"  &   �� 5#  *   $	��12* �68   �� 5$  *   $	��12* �68   .+�5'  &   ]� ��.  $��	10,2,10,2q�6F   .+�5(  *   � $��Bottom=�6/    5)  .   �� 5*  2   $��8* �6C   �� 5+  2   $	��10* �6C   �� 5,  2   $��3* �6C   �� 5-  2   $��2* �6C   .+�5/  .   ��  $  MaterialDesignHeadline5TextBlock#��[ 68   $��卖一��6r   $��0��6|   ���#2E8B576�   �� 50  .   #��[ 68   $��1��6�   ���#2E8B57��6r   �� *ShowHQ.SelPrice1+6�   �� 51  .   #��[ 68   $��2��6�   ���#2E8B57��6r   �� *ShowHQ.SelVol1+6�   �� 52  .   #��[ 68   $	��手��6r   $��3��6|   ���#2E8B576�   53  +   5p  '   ]� 5r  &   ��.  $��	10,2,10,2q�6F   $��1��6Y   .+�5s  *   � $	��Top=�6/    5t  .   �� 5u  2   $��8* �6C   �� 5v  2   $	��10* �6C   �� 5w  2   $��3* �6C   �� 5x  2   $��2* �6C   .+�5z  .   �� #��[ 68   $��买一��6r   $��0��6|   ���#FF45006�   �� 5{  .   #��[ 68   $��1��6�   ���#FF4500��6r   �� *ShowHQ.BuyPrice1+6�   �� 5|  .   #��[ 68   $��2��6�   ���#FF4500��6r   �� *ShowHQ.BuyVol1+6�   �� 5}  .   #��[ 68   $	��手��6r   $��3��6|   ���#FF45006�   5~  +   5�  '   5�  #   5�     5�     �� 5�     $��1q�6!   
�������$��0,0,0,5q�6H   $��2��6Y   5�     5�     5�     5�     �� 5�     -   5        $��PairItem��5�     $��88��6&   $��配對��61   $��50��6>   #��[ 6J   $
+ Hand��6   .��5�     � 
�������.+�5�     �  5�     �� 5�     $	��18* �6,   �� 5�     $��6* �6,   5�     �� 5�     $��1q�6   
�������$��0,0,0,5q�6D   ._�5�     ]� ��.  .+�5�     �� $��0q�6%   
�������$	��Top=�6M   ._�5�  "   ]� $��20,10,20,10q�6,   $��Bottom=�6A   .+�5�  &   �� -   5        $
��Left=�5�  /   #��[ 6E   $��5q�6�   $0 多头比例��6�   $��20(�6�   .��6�   ��5�  *   �� 5�  .   $6 	LongRatio�67   $7 PropertyChanged=�6I   8 5�  2     5�  6   $9 	LongRatio��6W   $: 1~�6m   $; 1~�6w   $< 10~�6   5�  /   .��5�  8   5�  '   �� 5�  &   -   5        $
��Left=�5�  /   #��[ 6E   $��5q�6�   $0 价格乘数��6�   $��20(�6�   .��6�   ��5�  *   �� 5�  .   $6 LongMultiplier�67   $7 PropertyChanged=�6N   8 5�  2     5�  6   $9 LongMultiplier��6W   $: 1~�6r   $; 1~�6|   $< 10~�6�   5�  /   .��5�  8   5�  '   �� 5�  &   -   5        $0 多头代码��5�  /   $	1 .26Q�5�  +   
���   �$
2 Code��5  +   3 . $
��Left=�5  >   $��30,5,5,5q�5  +   ��5   +   �� *
CodeList+4 5  +   �� *SelectedLongCodeItem+��5  *   �� 5  .   .R�5  2   $� 5	  /   ��5  *   �� 5  .   .R�5  6   ]� ��.  .+�5  :   �� $
��Left=�6E   $��15(�6q   ��6[   �� *Code+6q   �� 5  :   $��Right=�6E   $��15(�6r   ��6\   �� *Name+6r   5  7   5  /   5  '   5  #   5     �� 5     $��0q�6%   
�������$	��Top=�6M   ._�5  "   ]� $��20,10,20,10q�6,   $��Bottom=�6A   .+�5  &   �� -   5        $
��Left=�5  /   #��[ 6E   $��5q�6�   $0 空头比例��6�   $��20(�6�   .��6�   ��5  *   �� 5  .   $6 
ShortRatio�67   $7 PropertyChanged=�6J   8 5  2     5  6   $9 
ShortRatio��6W   $: 1~�6n   $; 1~�6x   $< 10~�6�   5   /   .��5!  8   5"  '   �� 5#  &   -   5        $
��Left=�5#  /   #��[ 6E   $��5q�6�   $0 价格乘数��6�   $��20(�6�   .��6�   ��5$  *   �� 5%  .   $6 ShortMultiplier�67   $7 PropertyChanged=�6O   8 5&  2     5'  6   $9 ShortMultiplier��6W   $: 1~�6s   $; 1~�6}   $< 10~�6�   5)  /   .��5*  8   5+  '   �� 5,  &   -   5        $0 空头代码��5,  /   $	1 .26Q�5-  +   
���   �$
2 Code��51  +   3 . $
��Left=�52  >   $��30,5,5,5q�54  +   ��5/  +   �� *
CodeList+4 50  +   �� *SelectedShortCodeItem+��56  *   �� 57  .   .R�58  2   $� 59  /   ��5;  *   �� 5<  .   .R�5>  6   ]� ��.  .+�5?  :   �� $
��Left=�6E   $��15(�6q   ��6[   �� *Code+6q   �� 5@  :   $��Right=�6E   $��15(�6r   ��6\   �� *Name+6r   5A  7   5C  /   5E  '   5F  #   5G     ]� 5H     $��20,10,20,10q�6(   $	��Top=�6=   $��Bottom=�6R   .+�5I  "   �� $
��Left=�6,   $��价差:��6B   $��20(�6M   
���   �$��5,40,5,5q�6n   $��Center=�6�   $��Center=�6�   �� 5J  "   #��[ 5L  %   $��Center=�5M  %   $��30,5,5,5q�5O  %   ��5K  %   �� $6 
MinusPrice�.  (MaterialDesignThemes.Wpf.TextFieldAssistA   
SuffixTextA 5N  %   �� *CurrentMinusPrice+5O  %   5Q     ]� 5R     $��20,10,20,10q�6(   $	��Top=�6=   $��Bottom=�6R   .+�5S  "   �� $
��Left=�6,   $��对数:��6B   $��20(�6M   
���   �$��5,40,5,5q�6n   $��Center=�6�   $��Center=�6�   �� 5T  "   #��[ 6+   $��Center=�6f   $��30,5,5,5q�6�   .��6�   ��5U  &   �� 5V  *   $6 Pairs�63   $7 PropertyChanged=�6A   8 5W  .     5X  2   $9 Pairs��6S   $: 1~�6f   $; 1~�6p   $	< 100~�6x   5Z  +   .��5[  4   5\  #   5]     �� 5_     $	��Top=�6H   $��20,10,20,10q�6]   -  'MaterialDesignThemes.Wpf.CheckBoxAssistB   CheckBoxSize$B 20Q�6s   $��20(�6�   . 6'   �� *
IsOnlyOpen+.��6�   只開倉不平倉6�   �� 5`     $��20,10,20,50q�6%   $= 10��6:   $��Bottom=�6i   ��6�   �� *PairCommonNewOrderCommand+.��6�   提交訂單6�   5a     5b     5c     5d     �� 5e     -   5        $��	OtherItem��5e     $��88��6'   $��其他��62   $��50��6?   #��[ 6K   $
+ Hand��6�   $y�Hidden=�6�   .��5f     � 
�������.+�5g     �� C �� TabStripPlacement$
C Left=�6!   .@�5i     �� $��	
釣
魚
��6"   #��[ 6@   .��5k     	  $��8q�62   .��5l  "   ]� ��.  .+�5m  &   �� $	��Top=�61   .+�5n  *   �� $��
16 16 12 8q�64   $��16(�6H   &$參數設置 (買賣價格可編輯)6f   &   System.Windows.Controls.DataGrid  5o  *   D   CanUserAddRowsD .  E   AutoGenerateColumnsE .  F   SelectionUnit$
F Cell=�6�   G   SelectionMode$G Extended=�6�   ��64   �� *
FishList+H   ColumnsH 5p  .   4  .System.Windows.Controls.DataGridCheckBoxColumn  5q  2   ,  &System.Windows.Controls.DataGridColumnI   CanUserSortI .   - )MaterialDesignDataGridCheckBoxColumnStyle1  +System.Windows.Controls.DataGridBoundColumnJ   ElementStyle#J [ 5s  5    4 0MaterialDesignDataGridCheckBoxColumnEditingStyleK   EditingElementStyle#K [ 5t  5   L   BindingL 5r  5   �� *
IsSelected+$7 PropertyChanged=�M �� Mode$M TwoWay=�5t  5   0  *System.Windows.Controls.DataGridTextColumn  5u  2   N   Header$N 代码��6_   O   
IsReadOnlyO . L 6E   �� *SCode+6k     5v  2   $N 前收��6b   O . L 6E   �� *
PrePrice+6n     5w  2   $N 买价��6f   O .  L 6E   �� *BuyFishPrice+6r     5x  2   $N 卖价��6g   O .  L 6E   �� *SellFishPrice+6s   5z  +   5{  '   ]� 5|  &   $��20,50,20,50q�60   $	��Top=�6E   .+�5}  *   �� $
��Left=�64   $��数量:��6J   $��20(�6U   
���   �$��	0 25 25 0q�6v   $��Center=�6�   $��Center=�6�   �� 5~  *   #��[ 62   $��Center=�6m   .��6�   ��5  .   �� 5�  2   $6 
FishAmount�6;   $7 PropertyChanged=�6N   8 5�  6     5�  :   $9 amount��6[   $: 1~�6n   $; 1~�6x   $< 50~�6�   5�  3   .��5�  <   5�  +   5�  '   �� 5�  &   $	��Top=�6N   $��20,50,20,50q�6c    ( $MaterialDesignUserForegroundCheckBox#��[ 6x   . 6/   �� *
IsFishOk+.��6�   &$訂單要素已經確認正確無誤5�  '   �� 5�  &   $��20,10,20,50q�6-   $= 10��6B   $��Bottom=�6q   ��6�   �� *CommonSendFishOrders+{�6�   �� *
IsFishOk+.��6�   提交訂單6�   �� 5�  &   $= 10��6-   $��Bottom=�6[   $��20,10,20,50q�6s    $  MaterialDesignRaisedAccentButton#��[ 6�   ��6�   �� *CommonInitialFish+.��6�   	初始化6�   5�  #   5�     5�     5�     5�     5     5     � 5#  
   $��2��6   $��3��6    5$     �� 5%     $	��17* �6    �� 5&     $��7* �6    .+�5)     �  5*     �� 5+     -   5        $ 	MainCol_0��5+  H   ��6'   �� *MainColWidth_0+6H   �� 5,     -   5        $ 	MainCol_1��5,  H   ��6'   �� *MainColWidth_1+6H   .+�50     �  51     �� 52     $	��24* �6+   �� 53     $��0* �6+    55     �� 56     $	��10* �6(   �� 57     $��6* �6(   .+�59     ]� .+�5:     ]� $	��Top=�6$   .+�5;     �� $= 10��6%   $
��Left=�6T   $��2,2,20,0q�6j   ��6|   �� *CommonCancelOrder+.��6�   撤銷所選訂單6�   �� 5<     $= 10��6%   $��Right=�6S   $��	20,2,10,0q�6j   #��[ 6�   ��6}   �� *CommonCancelAllOrder+.��6�   全撤6�   �� 5=     $��1q�6%   
�������$��0,2,0,0q�6L   ._�5>  "   �� $��Center=�6-   $
��Left=�6H   P �� Orientation$P 
Horizontal=�6c   .+�5?  &   �� -   5        Q ��Name$Q TodealsLongCheck��5?  /   $��Center=�6I   $��多头��6e   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5@  &   -   5        $Q TodealsShortCheck��5@  /   $��Center=�6J   $��空头��6f   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5A  &   -   5        $Q TodealsPairs��5A  /   $��Center=�6E   $��配對��6a   $. FalseY�6�   $��	10,0,10,0q�6�   �� 5B  &   -   5        $5 TodealsBondName��5B  .   R �� CharacterCasing$R Upper=�5C  &   S   TextBoxViewMargin$S 1 0 1 0q�5D  &   $0 证券代码模糊匹配��5E  &   $��Center=�5F  &   T �� TextAlignment$T Center=�5G  &   $��16(�5H  &   $��Center=�5I  &   $��	10,0,10,0q�6A   6T   5J  #   5K     5L     �� 5N     $��1q�6!   
�������._�5O       -   5        U  Name$U AliveOrdersDataGrid��5O  '   D .  E .  $
F Cell=�6�   $G Extended=�6�   ��6D   �� V �� Source#V [ H 5P  "     5Q  &   I .  #J [ 5S  %   #K [ 5T  %   L 5R  %   �� *
IsSelected+$7 PropertyChanged=�$M TwoWay=�5T  %     5U  &   $N 代码��6R   O . L 69   �� *code+6^     5V  &   $N 方向��6:   O . L 6Y   �� *entrust_direction+  5W  &   $N 开平��6_   O . L 69   �� *futures_direction+6l     5X  &   $N 委托价格��6[   O . L 69   �� *entrust_price+6j     5Y  &   $N 委托数量��6\   O . L 69   �� *entrust_amount+6k     5Z  &   $N 成交数量��6Y   O . L 69   �� *deal_amount+6h     5\  &   $N 订单编号��6X   O . L 69   �� *
entrust_no+6g     5]  &   $N 其他信息��6V   O . L 69   �� *
addInfo1+6e     5_  &   $N 错误信息��6X   O . L 69   �� *
error_info+6g     5`  &   $N 發送時間��6V   O . L 69   �� *
addInfo3+6e   5c     5d     5e     ]� 5f     $��1��6    .+�5g     ]� $	��Top=�6$   .+�5h     �� #��[ 6(   $��0,0,20,0q�6c   结束委託6{   �� 5i     $��1q�6%   
�������._�5j  "   �� $��Center=�6-   $
��Left=�6H   $P 
Horizontal=�6c   .+�5k  &   �� -    5        $Q FinishedLongCheck��5k  /   $��Center=�6J   $��多头��6f   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5l  &   -!   5        $Q FinishedShortCheck��5l  /   $��Center=�6K   $��空头��6g   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5m  &   -"   5        $Q FinishedDeal��5m  /   $��Center=�6E   $��成交��6a   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5n  &   -#   5        $Q FinishedWithdraw��5n  /   $��Center=�6I   $��撤销��6e   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5o  &   -$   5        $Q FinishedInvalid��5o  /   $��Center=�6H   $��废单��6d   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5p  &   -%   5        $Q FinishedPairs��5p  /   $��Center=�6F   $��配對��6b   $. FalseY�6�   $��	10,0,10,0q�6�   �� 5q  &   -&   5        $5 FinishedBondName��5q  .   $R Upper=�5r  &   $S 1 0 1 0q�5s  &   $0 证券代码模糊匹配��5t  &   $��Center=�5u  &   $T Center=�5v  &   $��16(�5w  &   $��Center=�5x  &   $��	10,0,10,0q�6A   6T   5y  #   5z     5{     �� 5|     $��1q�6!   
�������._�5}       -'   5        $U FinishedDataGrid��5}  '   D .  E .  $
F Cell=�6�   $G Extended=�6�   ��6A   �� #V [ H 5~  "     5  &   $N 代码��6R   O . L 69   �� *code+6^     5�  &   $N 方向��6_   O . L 69   �� *entrust_direction+6l     5�  &   $N 开平��6_   O . L 69   �� *futures_direction+6l     5�  &   $N 委托价格��6[   O . L 69   �� *entrust_price+6j     5�  &   $N 委托数量��6\   O . L 69   �� *entrust_amount+6k     5�  &   $N 成交数量��6Y   O . L 69   �� *deal_amount+6h     5�  &   $N 成交均价��6X   O . L 69   �� *
deal_price+6g     5�  &   $N 订单状态��6X   O . L 69   �� *
orderState+6g     5�  &   $N 订单编号��6X   O . L 69   �� *
entrust_no+6g     5�  &   $N 其他信息��6V   O . L 69   �� *
addInfo1+6e     5�  &   $N 错误信息��6X   O . L 69   �� *
error_info+6g     5�  &   $N 發送時間��6V   O . L 69   �� *
addInfo3+6e   5�     5�     5�     5�     � 5�     $��1��6   .+�5�     �� $��0q�6   
�������._�5�     ]� .+�5�     ]� $	��Top=�6(   .+�5�  "   �� $= 10��6)   $
��Left=�6X   $��2,2,20,0q�6n   ��6�   �� *CommonCancelPariOrder+.��6�   撤銷配對訂單6�   �� 5�  "   $= 10��6)   $��Right=�6W   $��	20,2,10,0q�6n   #��[ 6�   ��6�   �� *CommonCancelAllPairsOrder+.��6�   全撤6�   �� 5�  "   $��1q�6)   
�������$��0,2,0,0q�6P   ._�5�  &   �� $��Center=�61   $
��Left=�6L   $P 
Horizontal=�6g   .+�5�  *   �� -(   5        $Q PairIsFinish��5�  3   $��Center=�6I   $��	已結束��6e   $
. TrueY�6�   $��	10,0,10,0q�6�   �� 5�  *   -)   5        $Q PairIsUNFinish��5�  3   $��Center=�6K   $��	未結束��6g   $
. TrueY�6�   $��	10,0,10,0q�6�   5�  '   5�  #   5�     �� 5�     $��1q�6%   
�������._�5�  "     -*   5        $U PairsDataGrid��5�  ,   D .  E .  $
F Cell=�6�   $G Extended=�6�   ��6C   �� #V [	 $7 PropertyChanged=�H 5�  &     5�  *   I .  #J [ 5�  %   #K [ 5�  %   L 5�  %   �� *
IsSelected+$7 PropertyChanged=�$M TwoWay=�5�  %   4  .System.Windows.Controls.DataGridTemplateColumn  5�  *   $N 	乘数比��6A   W   CellTemplateW 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   $��5,0,0,0q�6Z   .+�5�  :   �� 
���  ����6Y   �� *LongMultiplier+�� 5�  :   $	�� : ��6G   �� 5�  :   
��� � ���6[   �� *ShortMultiplier+5�  7   5�  3   5�  +     5�  *   $N 組合信息��6A   W 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   .+�5�  :   �� 
���  ��#��[ 6X   ��6�   �� *	LongRatio+�� 5�  :   
���  ��#��[ 6X   $	�� * ��6�   �� 5�  :   
���  ��#��[ 6X   ��6�   �� *
LongCode+�� 5�  :   #��[ 6F   $	�� - ��6�   �� 5�  :   
��� � �#��[ 6Z   ��6�   �� *
ShortRatio+�� 5�  :   
��� � �#��[ 6Z   $	�� * ��6�   �� 5�  :   
��� � �#��[ 6Z   ��6�   �� *	ShortCode+5�  7   5�  3   5�  +     5�  *   $N 委托价差��6W   O . L 6=   �� *Minus+6f     5�  *   $N 当前价差��6^   O . L 6=   �� *CurrentMinus+6m     5�  *   $N 成交价差��6Z   O . L 6=   �� *
MeanCost+6i     5�  *   $N 多頭成交��6A   W 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   .+�5�  :   �� 
���  ��#��[ 6X   ��6�   �� *FinishedLongLeg+5�  7   5�  3   5�  +     5�  *   $N 空頭成交��6A   W 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   .+�5�  :   �� 
��� � �#��[ 6Z   ��6�   �� *FinishedShortLeg+5�  7   5�  3   5�  +     5�  *   $N 已成/對數��6A   W 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   .+�5�  :   �� #��[ 6F   ��6�   �� *FinishedPairs+�� 5�  :   #��[ 6F   $��/��6�   �� 5�  :   #��[ 6F   ��6�   �� *Pairs+5�  7   5�  3   5�  +     5�  *   $N 進度��6A   W 5�  .   �� 5�  2   .R�5�  6   �� $P 
Horizontal=�6A   .+�5�  :   � $��10��6a   $��0,0,5,0q�6n   $��88��6   ;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssistX   DisableTransitionsX . Y � ValueY 6F   �� *
Progress+6�   �� 5�  :   ��6G   �� *
Progress+�� 5�  :   $��%��6G   5�  7   5�  3   5�  +     5�  *   $N 暂停发单��6A   W 5   .   �� 5  2   .R�5  6   d�  $  MaterialDesignSwitchToggleButton#��[ 6D   . 6   �� *	IsPause+$7 PropertyChanged=�$M TwoWay=�5  3   5  +     5  *   $N 是否结束��6Z   O . L 6=   �� *
IsFinish+6i     5  *   $N 是否撤单��6Z   O . L 6=   �� *
IsDelete+6i     5  *   $N 發送時間��6_   O . L 6=   �� *OrderSendTime+6n     5  *   $N 其他信息��6Z   O . L 6=   �� *
AddInfo2+6i   5  #   5     5     5     5     5     � 5     $��1��6    5     �� 5     $��6* �6'   �� 5     $��5* �6'   �� 5     $	��12* �6'   .+�5      ]� .+�5!     ]� $	��Top=�6    .+�5"     �� $
��Left=�6$   #��[ 6:   $��0,0,20,0q�6t   持倉6�   �� 5#     $��1q�6!   
�������._�5$     �� $��Center=�6)   $
��Left=�6D   $P 
Horizontal=�6_   .+�5%  "   �� -+   5        $Q amountCheck��5%  +   $��Center=�6@   $��显示0持仓��6\   $. FalseY�6�   $��2,0,2,0q�6�   �� 5&  "   -,   5        $Q 	longCheck��5&  +   $��Center=�6>   $��多头��6Z   $
. TrueY�6�   $��2,0,2,0q�6�   �� 5'  "   --   5        $Q 
shortCheck��5'  +   $��Center=�6?   $��空头��6[   $
. TrueY�6�   $��2,0,2,0q�6�   �� 5(  "   -.   5        $5 bondName��5(  *   $R Upper=�5)  &   $S 1 0 1 0q�5*  &   $0 代码匹配��5+  &   $��Center=�5,  &   $T Center=�5-  &   $��16(�5.  &   $��Center=�5/  &   $��0q�6A   6L   50     52     53     �� 54     $��1q�6   
�������$��0,0,0,5q�6D   ._�55       -/   5        $U AccountDataGrid��55  #   E .  D .  $
F Cell=�6�   $G Extended=�6�   Z   
IsReadOnlyZ . ��6<   �� #V [ H 56       57  "   $N 代码��6X   L 65   �� *out_stock_code+6X     58  "   $N 方向��6�   L 65   �� *out_position_flag+[ �� 	Converter#[ [ 6�     59  "   $N 可用持仓��6[   L 65   �� *out_enable_amount+6[     5:  "   $N 	总持仓��6\   L 65   �� *out_current_amount+6\   5@     5A     5B     ]� 5D     $��1��6   $��5,0,0,0q�6-   .+�5E     ]� $	��Top=�6    .+�5F     �� $
��Left=�6$   #��[ 6:   $��0,0,20,0q�6t   當日方向成本匯總6�   5G     �� 5H     $��1q�6   
�������$��0,0,0,5q�6D   ._�5I       E .  D .  $
F Cell=�6�   $G Extended=�6�   Z . ��6#   �� *DirectionSummaryCollection+H 5J       5K  "   $N 代码��6N   L 65   �� *code+6N     5L  "   $N 方向��6S   L 65   �� *	direction+6S     5M  "   $N 平均成本��6R   L 65   �� *
avgcostt+6R     5N  "   $N 持仓��6R   L 65   �� *
position+6R   5P     5Q     5R     ]� 5S     $��2��6   $��5,0,0,0q�6-   .+�5T     ]� $	��Top=�6    ��.  .+�5U     �� $
��Left=�6$   #��[ 6:   $��0,0,20,0q�6t   當日配對成本匯總6�   �� 5V     $��0,5,20,0q�6$   $��Right=�66   �� 5W     \ �� 	IsEnabled\ .     MaterialDesignBody1Hyperlink#��[ 6;   .�6q   更新頻率：3秒/次6}   5X     5Y     �� 5Z     $��1q�6   
�������$��0,0,0,5q�6D   ._�5[       E .  D .  $
F Cell=�6�   $G Extended=�6�   Z . ��6#   �� *PariSummaryCollection+H 5\       5]  "   $N 	乘数比��69   W 5^  &   �� 5_  *   .R�5`  .   �� $P 
Horizontal=�69   $��5,0,0,0q�6R   .+�5a  2   �� 
���  ����6Q   �� *LongMultiplier+�� 5b  2   $	�� : ��6?   �� 5c  2   
��� � ���6S   �� *ShortMultiplier+5d  /   5f  +   5h  #     5i  "   $N 組合��6O   L 65   �� *combo+6O     5j  "   $N 對數��6O   L 65   �� *pairs+6O     5l  "   $N 多頭��69   W 5m  &   �� 5n  *   .R�5p  .   �� 
���  ����6M   �� *
LongString+5q  +   5s  #     5u  "   $N 空頭��69   W 5v  &   �� 5w  *   .R�5y  .   �� 
��� � ���6O   �� *ShortString+5z  +   5|  #     5~  "   $N 平均基差��6O   L 65   �� *minus+6O     5�  "   $N 多头暴露��69   W 5�  &   �� 5�  *   .R�5�  .   �� $P 
Horizontal=�69   .+�5�  2   �� 
���  ����6Q   �� *LongExString+5�  /   5�  +   5�  #     5�  "   $N 空头暴露��69   W 5�  &   �� 5�  *   .R�5�  .   �� $P 
Horizontal=�69   .+�5�  2   �� 
��� � ���6S   �� *ShortExString+5�  /   5�  +   5�  #   5�     5�     5�     5�     5�     5�     5�     