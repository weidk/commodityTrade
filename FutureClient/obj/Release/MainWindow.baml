   M S B A M L   `   `   `  ���� G  CFutureClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FutureClientFutureClient  ?$clr-namespace:FutureClient.ConverterFutureClient.Converter  K*clr-namespace:FutureClient.ValidationRulesFutureClient.ValidationRules      FutureClient.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:FutureClient     :	converter$clr-namespace:FutureClient.Converter     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  @validationrules*clr-namespace:FutureClient.ValidationRules  -      MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  $	��800��$
��1200��$	��800��$
��1200�� � Icon$ icon.ico�� � Title �� *	MainTitle+ � 	Resources 0   *FutureClient.Converter.FutureNameConverter  fnConverter/   )FutureClient.Converter.DirectionConverter  directionConverter+   %FutureClient.Converter.ValueConverter  valueConverter  AccountView �� Source  FinishedView  ToDealsView 	 	PairsView  
 TabControlWithUnderLineStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 �Name J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation   TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name ��Name K� IsItemsHost   TabIndex K� ZIndex	 �� X1	 �� X2 �� RelativeSource ��Name   DirectionalNavigation ��Name �� SelectedContentTemplate �� SelectedContent" �� SelectedContentStringFormat �� ContentStringFormat �� Triggers  P� Property! P� Value" �� 
TargetName  TabItemExWithUnderLineStyle# ��Name$ � ToolTip% ^� 
Conditions& �� Binding' �� Value( �� AncestorLevel) �� AncestorType  TabItemStyle* �� Triggers��%�  &	       &	      &	 
     &	      &	 4     &	 _     &		 �     &	
 �     &	 #    &	           �� -    �� *AccountIpList+�� -    �� *ExpiredlatestIpList+�� -    �� *LatestIpList+�� -    �� *
PairList+�� " ��.���  � ��$ 2q���  � ��$ Center=���  � ��$ Center=���  � ��
 �������  � ��
 �������  � ��$ 1q���  � ��# � ����  � �� �� "	 ��.R�� $
 templateRoot��}�. #��z�� . $ Local=� �� $ ColumnDefinition0���� $ ColumnDefinition1��$��0 � �� $ RowDefinition0��$
��Auto ��� $ RowDefinition1��$��* �.+��� $ HeaderPanel��$��Center=�
������ $��0�� . $��0q�$��0��$ 1��$ 1���� $ 0��
�������$	��0.1��$��Bottom=�$��0 0 0 1q� .  �� *ActualWidth+ �� *Self+�� $ ContentPanel��#��z��#��z��#��z��$��0��$ 	Contained=�$��1��$ 2��$ Local=�._��� $ PART_SelectedContentHost��#��z #��z # z $��SelectedContent��$��0q�# z  P�   �@��TabStripPlacement$! Bottom=�. ��� $" HeaderPanel�� � ��$ 1���� $" ContentPanel�� � ��$ 0��P�   �@��TabStripPlacement$
! Left=�. ��� $" HeaderPanel�� � ��$ 0���� $" ContentPanel�� � ��$ 0���� $" HeaderPanel�� � ��$ 0���� $" ContentPanel�� � ��$ 1��P�   �@��TabStripPlacement$! Right=�. ��� $" HeaderPanel�� � ��$ 0���� $" ContentPanel�� � ��$ 0���� $" HeaderPanel�� � ��$ 1���� $" ContentPanel�� � ��$ 0��P�   � {�! .  . ��� $" templateRoot��# � �� � ���� " ��.���  � ��$ 30(���  � �� �#BEBEBE��  � ��
 ���� ��  � �� �#FFACAC��  � ��$ 0q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� "	 ��.R�� $
 templateRoot�� . 
������ .+��� $ 
_underline��
�������$��0q�#��z��� .+��� $	# txt��$y�Visible=�$��Center=�$��Center=�#��z��#$ z��#��z��$��CharacterEllipsis=� P�   �@��
IsSelected! . . ���  � ��$
 Bold"���  � ��
 �   �^� % �� & �� *IsMouseOver+ �� *Self+' true�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top.0��� $	" txt�� � �� �#FFFFFF^� % �� & �� *	IsEnabled+ �� *Self+	' false�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Left.0��� $" templateRoot�� �@J�Opacity$
 0.56Q�^� % �� & �� *	IsEnabled+ �� *Self+	' false�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��
' Bottom.0��� $" templateRoot�� �@J�Opacity$
 0.56Q�^� % �� & �� *	IsEnabled+ �� *Self+	' false�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��	' Right.0��� $" templateRoot�� �@J�Opacity$
 0.56Q�^� % �� & �� *	IsEnabled+ �� *Self+	' false�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top.0��� $" templateRoot�� �@J�Opacity$
 0.56Q�^� % �� & �� *
IsSelected+ �� *Self+' true�� & �� *TabStripPlacement+ �� *FindAncestor+$( 1��") ��' Top.0���  �@K�ZIndex$ 1���� $	" txt�� � �� �#FFFFFF�� $" 
_underline�� � ��$ 0 0 0 2q��� " ��* P�   �@��
IsSelected! .  . ���  � ��
 �������  � ��$ Light"�P�   �@��
IsSelected! . . ���  � �� �#632dc1��  � ��$
 Bold"�P�   �@J�IsMouseOver! . . ���  � �� �#632dc1.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 22(�.���  �� $
��166* ��� $	��82* ��� $	��61* ��� $	��86* ��� $
��797* �.+��� -   #��[
 ���	#FF632DC1
������ $��0q�$��2��.@��� -   $��DirectionItem��$��88��$��方向��$��50��#��[ + � Cursor$
+ Hand��.��� 
�������.+��  �� $	��10* ��� $	��14* ��� $��0* �.+��� $��1q����� *TraderColor+._�]� $��2 10 0 0q�.+�� $��	5 10 5 10q�$	��Top=� �� $��2* ��� $	��10* ��� $��2* ��� $	��10* � �� $��6* ��� $��6* ��� $��6* ��� $��6* �.+���  $  MaterialDesignSubtitle2TextBlock#��[ $��自動開平��$��0��$��4q�	� -   , 	�Name$, buyRadio��- 	� 	GroupName$- 	TradeSide��$��1��$��2��   MaterialDesignTabRadioButton#��[ ���#FF4500$��4q�. d� 	IsChecked$
. TrueY�$��买入��	� -	   $, 	sellRadio��$- 	TradeSide��$��1��$��2��$��2��#��[ ���#DCDCDC$��4q�$. FalseY�$��卖出���� #��[ $��手動開平��$��2��$��4q�	� -
   $, buyOpen��$- 	TradeSide��$��0��$��3��#��[ ���#DCDCDC$��4q�$. FalseY�$��买入开仓��	� -   $, buyClose��$- 	TradeSide��$��1��$��3��#��[ ���#DCDCDC$��4q�$. FalseY�$��买入平仓��	� -   $, sellOpen��$- 	TradeSide��$��2��$��3��#��[ ���#DCDCDC$��4q�$. FalseY�$��卖出开仓��	� -   $, 	sellClose��$- 	TradeSide��$��3��$��3��#��[ ���#DCDCDC$��4q�$. FalseY�$��卖出平仓��]� $��5 10 5 0q�$	��Top=�.+��� $
��Left=�$��代碼:��$��20(�
���   �$��0 0 25 0q�$��Center=�$��Center=��� -   / ��Name$/ codeCoboBox��)  #MaterialDesignThemes.Wpf.HintAssist0   Hint$0 请选择证券代码��1   HintOpacity$	1 .26Q�
���   �2 w� TextPath$
2 Code��3 �� 
IsEditable3 . ���� *
CodeList+4 �� SelectedItem4 �� *SelectedCodeItem+���� .R�$� ���� .R�]� ��.  .+��� $
��Left=�$��15(����� *Code+�� $��Right=�$��15(����� *Name+]� $��5 10 5 0q�$	��Top=�.+��� $
��Left=�$��价格:��$��20(�
���   �$��0 0 25 0q�$��Center=�$��Center=��� -   5 ��Name$5 
inputPrice�� % !MaterialDesignFloatingHintTextBox#��[ $��Center=�.������ 6 �� Path$6 Price�7 �� UpdateSourceTrigger$7 PropertyChanged=�.��]� $��5 10 5 0q�$	��Top=�.+��� $
��Left=�$��数量:��$��20(�
���   �$��0 0 25 0q�$��Center=�$��Center=��� -   $5 DirectionAmt��#��[ $��Center=�.������ $6 Amount�$7 PropertyChanged=�8 �� ValidationRules8 3   -FutureClient.ValidationRules.NumberValidation  9   	ValidType$9 amount��:   Ticks$: 1~�
;   Min$; 1~�
<   Max$< 50~�.���� $��34���� +  %MaterialDesignThemes.Wpf.ButtonAssist=   CornerRadius$= 10��$��Bottom=����� *TraderColor+���� *CommonNewOrderCommand+.��提交訂單�� $��1��  MaterialDesignGroupBox#���  $��0q�> � UseLayoutRounding> .  . ���� *ShowHQ.SCode+.��#	  MaterialDesignThemes.Wpf.Card	  +
  %MaterialDesignThemes.Wpf.ShadowAssist? 
  ShadowDepth$? Depth4=�@ 
  ShadowEdges$@ Bottom,Left=�$��5q�.��� $��0,0,0,0q� �� $	��12* ��� $	��12* �.+�]� ��.  $��	10,2,10,2q�.+�� $��Bottom=� �� $��8* ��� $	��10* ��� $��3* ��� $��2* �.+���  $  MaterialDesignHeadline5TextBlock#��[ $��卖一��$��0�����#2E8B57�� #��[ $��1�����#2E8B57���� *ShowHQ.SelPrice1+�� #��[ $��2�����#2E8B57���� *ShowHQ.SelVol1+�� #��[ $	��手��$��3�����#2E8B57]� ��.  $��	10,2,10,2q�$��1��.+�� $	��Top=� �� $��8* ��� $	��10* ��� $��3* ��� $��2* �.+��� #��[ $��买一��$��0�����#FF4500�� #��[ $��1�����#FF4500���� *ShowHQ.BuyPrice1+�� #��[ $��2�����#FF4500���� *ShowHQ.BuyVol1+�� #��[ $	��手��$��3�����#FF4500�� $��1q�
�������$��0,0,0,5q�$��2���� -   $��PairItem��$��88��$��配對��$��50��#��[ $
+ Hand��.��� 
�������.+��  �� $	��18* ��� $��6* ��� $��1q�
�������$��0,0,0,5q�._�]� ��.  .+��� $��0q�
�������$	��Top=�._�]� $��20,10,20,10q�$��Bottom=�.+��� -   $
��Left=�#��[ $��5q�$0 多头比例��$��20(�.������ $6 	LongRatio�$7 PropertyChanged=�8   $9 	LongRatio��$: 1~�$; 1~�$< 10~�.���� -   $
��Left=�#��[ $��5q�$0 价格乘数��$��20(�.������ $6 LongMultiplier�$7 PropertyChanged=�8   $9 LongMultiplier��$: 1~�$; 1~�$< 10~�.���� -   $0 多头代码��$	1 .26Q�
���   �$
2 Code��3 . $
��Left=�$��30,5,5,5q����� *
CodeList+4 �� *SelectedLongCodeItem+���� .R�$� ���� .R�]� ��.  .+��� $
��Left=�$��15(����� *Code+�� $��Right=�$��15(����� *Name+�� $��0q�
�������$	��Top=�._�]� $��20,10,20,10q�$��Bottom=�.+��� -   $
��Left=�#��[ $��5q�$0 空头比例��$��20(�.������ $6 
ShortRatio�$7 PropertyChanged=�8   $9 
ShortRatio��$: 1~�$; 1~�$< 10~�.���� -   $
��Left=�#��[ $��5q�$0 价格乘数��$��20(�.������ $6 ShortMultiplier�$7 PropertyChanged=�8   $9 ShortMultiplier��$: 1~�$; 1~�$< 10~�.���� -   $0 空头代码��$	1 .26Q�
���   �$
2 Code��3 . $
��Left=�$��30,5,5,5q����� *
CodeList+4 �� *SelectedShortCodeItem+���� .R�$� ���� .R�]� ��.  .+��� $
��Left=�$��15(����� *Code+�� $��Right=�$��15(����� *Name+]� $��20,10,20,10q�$	��Top=�$��Bottom=�.+��� $
��Left=�$��价差:��$��20(�
���   �$��5,40,5,5q�$��Center=�$��Center=��� #��[ $��Center=�$��30,5,5,5q����� $6 
MinusPrice�.  (MaterialDesignThemes.Wpf.TextFieldAssistA   
SuffixTextA �� *CurrentMinusPrice+]� $��20,10,20,10q�$	��Top=�$��Bottom=�.+��� $
��Left=�$��对数:��$��20(�
���   �$��5,40,5,5q�$��Center=�$��Center=��� #��[ $��Center=�$��30,5,5,5q�.������ $6 Pairs�$7 PropertyChanged=�8   $9 Pairs��$: 1~�$; 1~�$	< 100~�.���� $	��Top=�$��20,10,20,10q�-  'MaterialDesignThemes.Wpf.CheckBoxAssistB   CheckBoxSize$B 20Q�$��20(�. �� *
IsOnlyOpen+.��只開倉不平倉�� $��20,10,20,50q�$= 10��$��Bottom=����� *PairCommonNewOrderCommand+.��提交訂單�� -   $��	OtherItem��$��88��$��其他��$��50��#��[ $
+ Hand��$y�Hidden=�.��� 
�������.+��� C �� TabStripPlacement$
C Left=�.@��� $��	
釣
魚
��#��[ .��	  $��8q�.��]� ��.  .+��� $	��Top=�.+��� $��
16 16 12 8q�$��16(�&$參數設置 (買賣價格可編輯)&   System.Windows.Controls.DataGrid  D   CanUserAddRowsD .  E   AutoGenerateColumnsE .  F   SelectionUnit$
F Cell=�G   SelectionMode$G Extended=����� *
FishList+H   ColumnsH 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumnI   CanUserSortI .   - )MaterialDesignDataGridCheckBoxColumnStyle1  +System.Windows.Controls.DataGridBoundColumnJ   ElementStyle#J [  4 0MaterialDesignDataGridCheckBoxColumnEditingStyleK   EditingElementStyle#K [ L   BindingL �� *
IsSelected+$7 PropertyChanged=�M �� Mode$M TwoWay=�0  *System.Windows.Controls.DataGridTextColumn  N   Header$N 代码��O   
IsReadOnlyO . L �� *SCode+  $N 前收��O . L �� *
PrePrice+  $N 买价��O .  L �� *BuyFishPrice+  $N 卖价��O .  L �� *SellFishPrice+]� $��20,50,20,50q�$	��Top=�.+��� $
��Left=�$��数量:��$��20(�
���   �$��	0 25 25 0q�$��Center=�$��Center=��� #��[ $��Center=�.������ $6 
FishAmount�$7 PropertyChanged=�8   $9 amount��$: 1~�$; 1~�$< 50~�.���� $	��Top=�$��20,50,20,50q� ( $MaterialDesignUserForegroundCheckBox#��[ . �� *
IsFishOk+.��&$訂單要素已經確認正確無誤�� $��20,10,20,50q�$= 10��$��Bottom=����� *CommonSendFishOrders+{��� *
IsFishOk+.��提交訂單�� $= 10��$��Bottom=�$��20,10,20,50q� $  MaterialDesignRaisedAccentButton#��[ ���� *CommonInitialFish+.��	初始化� $��2��$��3�� �� $	��17* ��� $��7* �.+��  �� -   $ 	MainCol_0������ *MainColWidth_0+�� -   $ 	MainCol_1������ *MainColWidth_1+.+��  �� $	��24* ��� $��0* � �� $	��10* ��� $��6* �.+�]� .+�]� $	��Top=�.+��� $= 10��$
��Left=�$��2,2,20,0q����� *CommonCancelOrder+.��撤銷所選訂單�� $= 10��$��Right=�$��	20,2,10,0q�#��[ ���� *CommonCancelAllOrder+.��全撤�� $��1q�
�������$��0,2,0,0q�._��� $��Center=�$
��Left=�P �� Orientation$P 
Horizontal=�.+��� -   Q ��Name$Q TodealsLongCheck��$��Center=�$��多头��$
. TrueY�$��	10,0,10,0q��� -   $Q TodealsShortCheck��$��Center=�$��空头��$
. TrueY�$��	10,0,10,0q��� -   $Q TodealsPairs��$��Center=�$��配對��$. FalseY�$��	10,0,10,0q��� -   $5 TodealsBondName��R �� CharacterCasing$R Upper=�S   TextBoxViewMargin$S 1 0 1 0q�$0 证券代码模糊匹配��$��Center=�T �� TextAlignment$T Center=�$��16(�$��Center=�$��	10,0,10,0q��� $��1q�
�������._�  -   U  Name$U AliveOrdersDataGrid��D .  E .  $
F Cell=�$G Extended=����� V �� Source#V [ H   I .  #J [ #K [ L �� *
IsSelected+$7 PropertyChanged=�$M TwoWay=�  $N 代码��O . L �� *code+  $N 方向��O . L �� *entrust_direction+  $N 开平��O . L �� *futures_direction+  $N 委托价格��O . L �� *entrust_price+  $N 委托数量��O . L �� *entrust_amount+  $N 成交数量��O . L �� *deal_amount+  $N 订单编号��O . L �� *
entrust_no+  $N 其他信息��O . L �� *
addInfo1+  $N 错误信息��O . L �� *
error_info+  $N 發送時間��O . L �� *
addInfo3+]� $��1��.+�]� $	��Top=�.+��� #��[ $��0,0,20,0q�结束委託�� $��1q�
�������._��� $��Center=�$
��Left=�$P 
Horizontal=�.+��� -    $Q FinishedLongCheck��$��Center=�$��多头��$
. TrueY�$��	10,0,10,0q��� -!   $Q FinishedShortCheck��$��Center=�$��空头��$
. TrueY�$��	10,0,10,0q��� -"   $Q FinishedDeal��$��Center=�$��成交��$
. TrueY�$��	10,0,10,0q��� -#   $Q FinishedWithdraw��$��Center=�$��撤销��$
. TrueY�$��	10,0,10,0q��� -$   $Q FinishedInvalid��$��Center=�$��废单��$
. TrueY�$��	10,0,10,0q��� -%   $Q FinishedPairs��$��Center=�$��配對��$. FalseY�$��	10,0,10,0q��� -&   $5 FinishedBondName��$R Upper=�$S 1 0 1 0q�$0 证券代码模糊匹配��$��Center=�$T Center=�$��16(�$��Center=�$��	10,0,10,0q��� $��1q�
�������._�  -'   $U FinishedDataGrid��D .  E .  $
F Cell=�$G Extended=����� #V [ H   $N 代码��O . L �� *code+  $N 方向��O . L �� *entrust_direction+  $N 开平��O . L �� *futures_direction+  $N 委托价格��O . L �� *entrust_price+  $N 委托数量��O . L �� *entrust_amount+  $N 成交数量��O . L �� *deal_amount+  $N 成交均价��O . L �� *
deal_price+  $N 订单状态��O . L �� *
orderState+  $N 订单编号��O . L �� *
entrust_no+  $N 其他信息��O . L �� *
addInfo1+  $N 错误信息��O . L �� *
error_info+  $N 發送時間��O . L �� *
addInfo3+� $��1��.+��� $��0q�
�������._�]� .+�]� $	��Top=�.+��� $= 10��$
��Left=�$��2,2,20,0q����� *CommonCancelPariOrder+.��撤銷配對訂單�� $= 10��$��Right=�$��	20,2,10,0q�#��[ ���� *CommonCancelAllPairsOrder+.��全撤�� $��1q�
�������$��0,2,0,0q�._��� $��Center=�$
��Left=�$P 
Horizontal=�.+��� -(   $Q PairIsFinish��$��Center=�$��	已結束��$
. TrueY�$��	10,0,10,0q��� -)   $Q PairIsUNFinish��$��Center=�$��	未結束��$
. TrueY�$��	10,0,10,0q��� $��1q�
�������._�  -*   $U PairsDataGrid��D .  E .  $
F Cell=�$G Extended=����� #V [	 $7 PropertyChanged=�H   I .  #J [ #K [ L �� *
IsSelected+$7 PropertyChanged=�$M TwoWay=�4  .System.Windows.Controls.DataGridTemplateColumn  $N 	乘数比��W   CellTemplateW �� .R��� $P 
Horizontal=�$��5,0,0,0q�.+��� 
���  ������ *LongMultiplier+�� $	�� : ���� 
��� � ����� *ShortMultiplier+  $N 組合信息��W �� .R��� $P 
Horizontal=�.+��� 
���  ��#��[ ���� *	LongRatio+�� 
���  ��#��[ $	�� * ���� 
���  ��#��[ ���� *
LongCode+�� #��[ $	�� - ���� 
��� � �#��[ ���� *
ShortRatio+�� 
��� � �#��[ $	�� * ���� 
��� � �#��[ ���� *	ShortCode+  $N 委托价差��O . L �� *Minus+  $N 当前价差��O . L �� *CurrentMinus+  $N 成交价差��O . L �� *
MeanCost+  $N 多頭成交��W �� .R��� $P 
Horizontal=�.+��� 
���  ��#��[ ���� *FinishedLongLeg+  $N 空頭成交��W �� .R��� $P 
Horizontal=�.+��� 
��� � �#��[ ���� *FinishedShortLeg+  $N 已成/對數��W �� .R��� $P 
Horizontal=�.+��� #��[ ���� *FinishedPairs+�� #��[ $��/���� #��[ ���� *Pairs+  $N 進度��W �� .R��� $P 
Horizontal=�.+�� $��10��$��0,0,5,0q�$��88��;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssistX   DisableTransitionsX . Y � ValueY �� *
Progress+�� ���� *
Progress+�� $��%��  $N 暂停发单��W �� .R�d�  $  MaterialDesignSwitchToggleButton#��[ . �� *	IsPause+$7 PropertyChanged=�$M TwoWay=�  $N 是否结束��O . L �� *
IsFinish+  $N 是否撤单��O . L �� *
IsDelete+  $N 發送時間��O . L �� *OrderSendTime+  $N 其他信息��O . L �� *
AddInfo2+� $��1�� �� $��6* ��� $��5* ��� $	��12* �.+�]� .+�]� $	��Top=�.+��� $
��Left=�#��[ $��0,0,20,0q�持倉�� $��1q�
�������._��� $��Center=�$
��Left=�$P 
Horizontal=�.+��� -+   $Q amountCheck��$��Center=�$��显示0持仓��$. FalseY�$��2,0,2,0q��� -,   $Q 	longCheck��$��Center=�$��多头��$
. TrueY�$��2,0,2,0q��� --   $Q 
shortCheck��$��Center=�$��空头��$
. TrueY�$��2,0,2,0q��� -.   $5 bondName��$R Upper=�$S 1 0 1 0q�$0 代码匹配��$��Center=�$T Center=�$��16(�$��Center=�$��0q��� $��1q�
�������$��0,0,0,5q�._�  -/   $U AccountDataGrid��E .  D .  $
F Cell=�$G Extended=�Z   
IsReadOnlyZ . ���� #V [ H   $N 代码��L �� *out_stock_code+  $N 方向��L �� *out_position_flag+[ �� 	Converter#[ [   $N 可用持仓��L �� *out_enable_amount+  $N 	总持仓��L �� *out_current_amount+]� $��1��$��5,0,0,0q�.+�]� $	��Top=�.+��� $
��Left=�#��[ $��0,0,20,0q�當日方向成本匯總�� $��1q�
�������$��0,0,0,5q�._�  E .  D .  $
F Cell=�$G Extended=�Z . ���� *DirectionSummaryCollection+H   $N 代码��L �� *code+  $N 方向��L �� *	direction+  $N 平均成本��L �� *
avgcostt+  $N 持仓��L �� *
position+]� $��2��$��5,0,0,0q�.+�]� $	��Top=���.  .+��� $
��Left=�#��[ $��0,0,20,0q�當日配對成本匯總�� $��0,5,20,0q�$��Right=��� \ �� 	IsEnabled\ .     MaterialDesignBody1Hyperlink#��[ .�更新頻率：3秒/次�� $��1q�
�������$��0,0,0,5q�._�  E .  D .  $
F Cell=�$G Extended=�Z . ���� *PariSummaryCollection+H   $N 	乘数比��W �� .R��� $P 
Horizontal=�$��5,0,0,0q�.+��� 
���  ������ *LongMultiplier+�� $	�� : ���� 
��� � ����� *ShortMultiplier+  $N 組合��L �� *combo+  $N 對數��L �� *pairs+  $N 多頭��W �� .R��� 
���  ������ *
LongString+  $N 空頭��W �� .R��� 
��� � ����� *ShortString+  $N 平均基差��L �� *minus+  $N 多头暴露��W �� .R��� $P 
Horizontal=�.+��� 
���  ������ *LongExString+  $N 空头暴露��W �� .R��� $P 
Horizontal=�.+��� 
��� � ����� *ShortExString+