   M S B A M L   `   `   `  ���� E  ASmartShare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SmartShare
SmartShare      SmartShare.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:SmartShare     -     � Title$  
MainWindow��$	��250��$	��300�� J� 	AllowDrop . $   SmartShare.LoadUnloadBehaviour   
LoadUnload .  � 	Resources  �� DataType#   SmartShare.AddClientViewModel   SmartShare.AddClient%   SmartShare.DropContentViewModel   SmartShare.DropContent��%D   (��       *, +)(��      *, +)�� "  .R�  �� "  .R�  .���  � RowDefinitions �� $	��20* ��� $	��20* ��� $	��80* � � ColumnDefinitions �� $	��80* ��� $	��20* ��� $	��20* �.+��� $��0��$��3��$��2q�
���   ��� $��2��$
��Left=�$	��230��$��25��$��4,0,0,0q����� *SharedDirectory+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��2��$��25��$��30��$	��...������ *BrowseSharedDirCommand+�� $��1��$��3��$��2q�
���   ��� $��1��$��0��$	��180��$��25��	 �� DisplayMemberPath$	 FriendlyName��
�������$��3,0,0,0q�$
��Left=����� *	Clients+$ OneWay=�$ PropertyChanged=�
 �� SelectedItem
 �� *SelectedClient+$ TwoWay=�$ PropertyChanged=��� $��1��$��1��$��25��$��Center=�$��30��
�������$��+������ *AddButtonCommand+�� $��1��$��2��$��25��$��Center=�$��30��
�������$��R������ *ReloadButtonCommand+�� $��2��$��3��$��2q�
���   ��� $��2��$��3������ *CurrentView+