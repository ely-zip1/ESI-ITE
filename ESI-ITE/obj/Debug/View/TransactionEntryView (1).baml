   M S B A M L   `   `   `  ����B  >ESI-ITE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ESI_ITE.ViewModel.CommandESI_ITE.ViewModel.Command  5      +clr-namespace:ESI_ITE.ViewESI_ITE.View  5      5clr-namespace:ESI_ITE.ViewModelESI_ITE.ViewModel  5      '    !ESI_ITE.View.TransactionEntryView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8comm'clr-namespace:ESI_ITE.ViewModel.Command     5      ,localclr-namespace:ESI_ITE.View     5      .vmclr-namespace:ESI_ITE.ViewModel     5      ?Custom,http://metro.mahapps.com/winfx/xaml/controls     5	      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bsi9http://schemas.microsoft.com/expression/2010/interactions  5      $��382.007��5   '   
�������  � DataContext  5      1   +ESI_ITE.ViewModel.TransactionEntryViewModel  5   
   -   5        	viewModel��5   '   .��5      � $��0��6   $��0��6    � ColumnDefinitions 5   
   �� 5      $	��52* �6   �� 5      $	��61* �6    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5       $��35 �6   �� 5!      $��35 �6   �� 5"      $��35 �6   �� 5#      $��35 �6   �� 5$      $��35 �6   �� 5%      $��35 �6   �� 5&      $��35 �6   �� 5'      $��35 �6   �� 5(      $��35 �6   .+�5-   
   �� $��10,8,0,9q�6D   $
��Left=�5.      $��Center=�6   $��26��6:   $
��auto��6F   $��Transaction Number��6S    �� Target 5-      ��  �� ElementName$ cmbTransactionNumber��5.   S   �� 5/   
   -   5         ��Name$ cmbTransactionNumber��5/      $��1��61   $��20��6A   $��Stretch=�50      $��Center=�6"   $��0q�6=   ��51      �� *CmbTransactionNumbers+ �� SelectedValue 52      �� *SelectedTransactionNumber+ �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=��� 56   
   $��10,9,0,8q�6B   $��1��6T   $
��Left=�57      $��Center=�6   $��26��6:   $
��auto��6F   $��Transaction Type��6S    56      �� $ cmbTransactionType��57   S   �� 58   
   -   5        $ cmbTransactionType��58      $��1��6/   $��1��6?   $��20��6L   $��Stretch=�59      $��center=�6"   $��0q�6=   ��5:      �� *CmbTransactionType+ 5;      �� *SelectedTransactionType+	 �� Mode$	 TwoWay=� . 
 �� SelectedIndex
 5<      �� *IndexTransactionType+$	 TwoWay=�.  (System.Windows.Interactivity.Interaction   Triggers 5=      /  )System.Windows.Interactivity.EventTrigger  5>         	EventName$ DropDownOpened��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5?      >  8Microsoft.Expression.Interactivity.Core.CallMethodAction     
MethodName$ ToggleFirstLoad��6C      TargetObject 6*   �� 6C   5@      ��5B      �� 5C      .R�5D      �  � Orientation$ 
Horizontal=�6    $��14��69   .+�5E      �� $��25��69   $��10(�6G   $
��Left=�6U   $	��Top=�6p   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *Code+6�   �� 5F      $��0q�6@   $��10(�6L   $	��Top=�6Z   $
��Left=�6r   ��6    �� *Description+6r   5G      5H      5J      �� 5N   
   $��10,8,0,9q�6A   $��2��6S   $
��Left=�5O      $��Center=�6   $��26��6:   $
��auto��6F   $��Document Number��6S    5N      �� $ txtDocumentNumber��5O   S   �� 5P   
   -   5         ��Name$ txtDocumentNumber��5P      $��1��6-   $��2��6=   $��20��6J   $��Stretch=�5Q      $��center=�6"   $��0q�6=   ��6H   �� *DocumentNumber+ . $ PropertyChanged=��� 5U   
   $��10,9,0,8q�6B   $��3��6T   $
��Left=�5V      $��Center=�6   $��26��6:   $
��auto��6F   $��Transaction Date��6S    5U      �� $ txtTransactionDate��5V   S   �� 5W   
   -   5        $ txtTransactionDate��5W      $��1��6.   $��3��6>   $��20��6K   $��Stretch=�5X      $��center=�6"   $��0q�6=    �� 
IsReadOnly . ��5Y      �� *TransactionDate+$	 OneWay=� . 6N   �� 5]   
   $��10,8,0,9q�6B   $��4��6T   $
��Left=�5^      $��Center=�6   $��26��6:   $
��auto��6F   $��Source Warehouse��6S    5]      �� $ cmbSourceWareHouse��5^   S   �� 5_   
   -   5        $ cmbSourceWareHouse��5_      $��1��6/   $��4��6?   $��20��6L   $��Stretch=�5`      $��center=�6"   $��0q�6=   ��5a      �� *CmbWarehouse+ 5b      �� *SelectedSourceWarehouse+ . 
 5c      �� *IndexSourceWarehouse+$	 TwoWay=���5d      �� 5e      .R�5f      � $ 
Horizontal=�6    $��14��69   .+�5g      �� $��25��69   $��10(�6G   $
��Left=�6U   $	��Top=�6p   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *Code+6�   �� 5h      $��0q�6=   $��10(�6I   $	��Top=�6W   $
��Left=�6o   ��6    �� *
Location+6o   5i      5j      5l      �� 5p   
   $��10,9,0,8q�6B   $��5��6T   $
��Left=�5q      $��Center=�6   $��26��6:   $
��auto��6F   $��Location��6S    5p      �� $ cmbTransactionType��5q   S   �� 5r   
   -   5        $ cmbSourceLocation��5r      $��1��6.   $��5��6>   $��20��6K   $��Stretch=�5s      $��center=�6"   $��0q�6=   ��5t      �� *CmbLocation+ 5u      �� *SelectedSourceLocation+ . 
 5v      �� *IndexSourceLocation+$	 TwoWay=���5w      �� 5x      .R�5y      � $ 
Horizontal=�6    $��14��69   .+�5z      �� $��25��69   $��10(�6G   $
��Left=�6U   $	��Top=�6p   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *Code+6�   �� 5{      $��0q�6=   $��10(�6I   $	��Top=�6W   $
��Left=�6o   ��6    �� *
Location+6o   5|      5}      5      �� 5�   
   $��10,8,0,9q�6   $��6��6"   $
��Left=�5�      $��Center=�6   $��26��6:   $
��auto��6F   $��Salesman��6S   �� 5�   
   $��10,9,0,8q�6@   $��7��6R   $
��Left=�5�      $��Center=�6   $��26��6:   $
��auto��6F   $��Select Price Category��6S    5�      �� $ cmbPriceCategory��5�   S   �� 5�   
   -   5        $ cmbPriceCategory��5�      $��1��6-   $��7��6=   $��20��6J   $��Stretch=�5�      $��center=�6"   $��0q�6=   ��6H   �� *PriceCategoryList+ 5�      �� *SelectedPriceCategory+ . 
 5�      �� *IndexPriceCategory+$	 TwoWay=��� 5�   
   $��10,8,0,9q�6<   $��8��6N   $
��Left=�5�      $��Center=�6   $��26��6:   $
��auto��6F   $��Select Price��6S    5�      �� $ cmbPriceType��5�   S   �� 5�   
   -	   5        $ cmbPriceType��5�      $��1��6)   $��8��69   $��20��6F   $��Stretch=�5�      $��center=�6"   $��0q�6=   ��6H   �� *PriceTypeList+ 5�      �� *SelectedPriceType+ . 
 5�      �� *IndexPriceType+$	 TwoWay=��� 5�   
   $��10,9,0,8q�6G   $��9��6Y   $
��Left=�5�      $��Center=�6   $��26��6:   $
��auto��6F   $��Destination Warehouse��6S    5�      �� $ cmbDestinationWareHouse��5�   S   �� 5�   
   -
   5        $ cmbDestinationWareHouse��5�      $��1��64   $��9��6D   $��20��6Q   $��Stretch=�5�      $��center=�6"   $��0q�6=   ��6H   �� *CmbWarehouse+ 5�      �� *SelectedDestinationWarehouse+ . {�5�      �� *EnableDestination+$	 OneWay=�
 5�      �� *IndexDestinationWarehouse+$	 TwoWay=���5�      �� 5�      .R�5�      � $ 
Horizontal=�6    $��14��69   .+�5�      �� $��25��69   $��10(�6G   $
��Left=�6U   $	��Top=�6p   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *Code+6�   �� 5�      $��0q�6=   $��10(�6I   $	��Top=�6W   $
��Left=�6o   ��6    �� *
Location+6o   5�      5�      5�      �� 5�   
   $��10,0,0,0q�6F   $��10��6X   $
��Left=�5�      $��Center=�6   $��Location��6:    5�      �� $ cmbDestinationLocation��5�   :   �� 5�   
   -   5        $ cmbDestinationLocation��5�      $��1��63   $��10��6C   $��20��6Q   $��Stretch=�5�      $��center=�6"   ��6=   �� *CmbLocation+ 5�      �� *SelectedDestinationLocation+ . {�5�      �� *EnableDestination+$	 OneWay=�
 5�      �� *IndexDestinationLocation+$	 TwoWay=���5�      �� 5�      .R�5�      � $ 
Horizontal=�6    $��14��69   .+�5�      �� $��25��69   $��10(�6G   $
��Left=�6U   $	��Top=�6p   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *Code+6�   �� 5�      $��0q�6=   $��10(�6I   $	��Top=�6W   $
��Left=�6o   ��6    �� *
Location+6o   5�      5�      5�      �� 5�   
   $��10,0,0,0q�6   $��11��6"   $
��Left=�5�      $��Center=�6   $��Salesman��6:   �� 5�   
   $��10,0,0,0q�6=   $��12��6O   $
��Left=�5�      $��Center=�6   $��Reason Code��6:    5�      �� $ cmbReasonCode��5�   :   �� 5�   
   -   5        $ cmbReasonCode��5�      $��1��6*   $��12��6:   $��20��6H   $��Stretch=�5�      $��center=�6"   ��6=   �� *	CmbReason+ 5�      �� *SelectedReason+ . 
 5�      �� *IndexReason+$	 TwoWay=���5�      �� 5�      .R�5�      � $ 
Horizontal=�6    $��14��69   .+�5�      �� $��25��6?   $��10(�6M   $
��Left=�6[   $	��Top=�6v   $��22��6�   $��0q�6�   $��0q�6�   $
��Bold"�6�   $��18��6�   ��6    �� *
ReasonCode+6�   �� 5�      $��0q�6@   $��10(�6L   $	��Top=�6Z   $
��Left=�6r   ��6    �� *Description+6r   5�      5�      5�      �� 5�   
   $��10,0,0,0q�6:   $��13��6L   $
��Left=�5�      $��Center=�6   $��Comment/ Ref. No.��6:    5�      �� $ 
txtComment��5�   :   �� 5�   
   -   5        $ 
txtComment��5�      $��1��6&   $��13��66   $��20��6D   $��Stretch=�5�      $��center=�6"   ��5�      �� *	Comment+$	 TwoWay=�$ 	LostFocus=�� 5�   
   $��2��6   $��14��6#    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� $��Right=�6   $��Center=�61   $��0,0,20,0q�5�      $��5q�6   $��
Line Items��6#   {�68   �� *	LineItems+�� 5�      $��1��6   $
��Left=�6%   $��Center=�6@   $��20,0,0,0q�5�      $��5q�6   $��Delete Entry��6#   5�      5�      5�      