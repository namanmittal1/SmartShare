   M S B A M L   `   `   `  ����E  ASmartShare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SmartShare
SmartShare  5   	   K GSmartShareCommon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:SmartShareCommon;assembly=SmartShareCommonSmartShareCommon 5   	       SmartShare.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *localclr-namespace:SmartShare     5   	   Kcommon8clr-namespace:SmartShareCommon;assembly=SmartShareCommon     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��300��6)   $   SmartShare.LoadUnloadBehaviour   
LoadUnload . .  (System.Windows.Interactivity.Interaction   Triggers 5
      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *WindowClosing+5       � 	Resources 5       �� DataType#   SmartShare.AddClientViewModel   SmartShare.AddClient%   SmartShare.DropContentViewModel	   SmartShare.DropContent��%�   (��       5      *, +)(�� 2     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��10* �6   �� 5       $	��20* �6   �� 5!      $	��20* �6   	 � ColumnDefinitions	 5#   
   �� 5$      $	��80* �6   �� 5%      $	��20* �6   �� 5&      $	��20* �6   .+�5)   
   �� $��0��6   $��3��6   $��2q�62   
���   �6\   �� 5*   
   $��2��6   $
��Left=�5+      $	��230��6-   $��25��69   $��4,0,0,0q�6E   ��5*   $   �� *SharedDirectory+
 �� Mode$
 OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�5+   E   �� 5,   
   $��2��6   $��25��6L   $��30��6X   $	��...��6c   ��6!   �� *BrowseSharedDirCommand+6c   �� 5-   
   $��1��6   $��3��6   $��2q�62   
���   �6\   �� 5.   
   $��1��6   $��0��6    $	��180��60   $��25��6<    �� DisplayMemberPath$ FriendlyName��51      
�������$��3,0,0,0q�52   *   $
��Left=�6;   ��5/      �� *	Clients+$
 OneWay=�$ PropertyChanged=� �� SelectedItem 50      �� *SelectedClient+$
 TwoWay=�$ PropertyChanged=�52   ;   �� 53   
   $��1��6   $��1��6   $��25��6.   $��Center=�6:   $��30��6W   
�������$��+��6u   ��54      �� *AddButtonCommand+�� 55   
   $��1��6   $��2��6   $��25��6.   $��Center=�6:   $��30��6W   
�������$��R��6u   ��56      �� *ReloadButtonCommand+�� 58   
   $��2��6   $��3��6   $��2q�62   
���   �6\   �� 5:   
   $��2��6   $��3��6'   ��6;   �� *CurrentView+�� 5<   
   $��3��6   $��3��6    $	��200��64   $��25��6@   $ HostName��5?      
�������$��3,0,0,0q�5@   *   $
��Left=�6;   ��5=      �� *Hosts+$
 OneWay=�$ PropertyChanged=� 5>      �� *SelectedHost+$
 TwoWay=�$ PropertyChanged=�5@   ;   �� 5B   
   $��3��6   $��2��6!   $��	Typing...��61   y�6B   �� *IsTypingVisible+�� 5D   
   $��4��6   $��2��6    '
   !SmartShare.TypingProgressBehavior 
  IsTyping 64   �� *IsTypingCommand+��5E      �� *
SendText+$
 OneWayToSource=�$ PropertyChanged=��� 5G   
   $��=>��6   $��4��6   $��3��6+   ��6;   �� *SendChatCommand+�� 5I   
   $��5��6   $��3��6    ��. $��Disabled=�5J      ��5K      �� *Chats+$
 TwoWay=�$ PropertyChanged=���5L      �� 5M      .R�5N      �  5O      �� 5P      $
��Auto �6,   	 5R      �� 5S      $��* �6/   �� 5T      $��* �6/   .+�5V      �� $
��Bold"�6E   ��6$   ��  �� Path$ 
SenderName�6E   �� 5W      $��1��6C   $
��Wrap=�6S   ��6$   �� $ ChatText�6S   5X      5Y      ��5\      �� 5]       �� 
TargetType" ��6    �� Triggers 5^      �� 5_        SmartShareCommon.ChatType   Sent �� Value# Z 6D    �� Binding 6&   �� $
 Type�.a�5`      ��  �� Property � �� �� Value$
 Left=�6E   5a      �� 5b         Recieved# Z 6D    6&   �� $
 Type�.a�5c      ��  � ��$ Right=�6E   5d      5f      5h      5i      5j      