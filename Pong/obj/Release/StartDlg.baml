   M S B A M L   `   `   `  ����   Pong    DlgMenuDemo.StartDlg   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ StartDlg��$	��400��$	��340�� � 
ResizeMode$ CanMinimize=�.���� .+��� $��Radius��$
��Left=�$	��Top=� �� Left$ 11��
 �� Top$ 45���� -    ��Name$ radius��$
��Left=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=�$��35��$	 115��$ 48��$��10���� -    ��Name$ OK��$��OK��$ 10��$	 326��$��75�� �� 	IsDefault . �� -   $ cancel��$��Cancel��$	 167��$	 326��$��75��	 �� IsCancel	 . �� $��(0 < Radius < 21)��$	 167��$ 45���� $��Paddel Laenge��$
��Left=�$	��Top=�$ 11��$ 71���� -   $ paddel��$
��Left=�$��23��$
 Wrap=�$	��Top=�$��35��$	 115��$ 74��$��80���� $��(49 < Laenge < 181)��$	 167��$ 71���� $��	Win Score��$
��Left=�$	��Top=�$ 11��$ 97���� -   $ Score��$
��Left=�$��23��$
 Wrap=�$	��Top=�$��35��$	 115��$	 100��$��10���� $��(0 < Score < 101)��$	 167��$ 97���� -   
 ��Name$
 
cmb_farben��$	 115��$	 128��$	��169���� $��Farbset��$
��Left=�$	��Top=�$ 11��$	 124��#  Xceed.Wpf.Toolkit.ColorPicker  -   $��ClrPcker_Paddel��$	 441��$ 50��$��16��$��92��   SelectedColor$	 RedX�$y�Hidden=�  -	     Name$ ClrPcker_Ball��$	 441��$ 75��$��16��$��92��$
 BlueX�$y�Hidden=�  -
   $ ClrPcker_Spielfeldrand��$	 441��$	 100��$��16��$��92��$ DarkGrayX�$y�Hidden=�  -   $ ClrPcker_Background��$	 441��$	 125��$��16��$��92��$ WhiteX�$y�Hidden=�  -   $ ClrPcker_Uhrfill��$	 441��$	 150��$��16��$��92��$ WhiteX�$y�Hidden=�  -   $ ClrPcker_Uhrstroke��$	 441��$	 175��$��16��$��92��$ BlackX�$y�Hidden=�  -   $ ClrPcker_Sekz��$	 441��$	 200��$��16��$��92��$ BlackX�$y�Hidden=�  -   $ ClrPcker_Minz��$	 441��$	 225��$��16��$��92��$ BlackX�$y�Hidden=�  -   $ ClrPcker_Stuz��$	 441��$	 250��$��16��$��92��$ BlackX�$y�Hidden=�  -   $ ClrPcker_hud��$	 441��$	 275��$��16��$��92��$ BlackX�$y�Hidden=��� -    ��Name$ l1��$��Paddel Farbe��$
��Left=�$	��Top=�$	 310��$ 45��$y�Hidden=��� -   $ l2��$��
Ball Farbe��$
��Left=�$	��Top=�$	 310��$ 70��$y�Hidden=��� -   $ l3��$��Spielfeldrand Farbe��$
��Left=�$	��Top=�$	 310��$ 95��$y�Hidden=��� -   $ l4��$��Background Farbe��$
��Left=�$	��Top=�$	 310��$	 120��$y�Hidden=��� -   $ l5��$��	Uhr Farbe��$
��Left=�$	��Top=�$	 310��$	 145��$y�Hidden=��� -   $ l6��$��Uhrrand Farbe��$
��Left=�$	��Top=�$	 310��$	 170��$y�Hidden=��� -   $ l7��$��Sekunden Zeiger Farbe��$
��Left=�$	��Top=�$	 310��$	 195��$y�Hidden=��� -   $ l8��$��Minuten Zeiger Farbe��$
��Left=�$	��Top=�$	 310��$	 220��$y�Hidden=��� -   $ l9��$��Stunden Zeiger Farbe��$
��Left=�$	��Top=�$	 310��$	 245��$y�Hidden=��� -   $	 l10��$��	HUD Farbe��$
��Left=�$	��Top=�$	 310��$	 270��$y�Hidden=��� -    ��Name$ 
check_ball��$#��Ball zurücksetzen nach punkt��$	 115��$	 161��$��20��$	��182���� -   $ check_paddel��$��Automatisches Paddel��$	 115��$	 186��$��20��$	��182���� -   $ 
Player_one��$��23��$	 115��$
 Wrap=�$��
Player One��$	 211��$	��120���� -   $ 
Player_two��$��23��$	 115��$
 Wrap=�$��
Player Two��$	 239��$	��120���� $��Name Player One��$ 11��$	 207���� -    $ l_player_two��$��Name Player Two��$ 11��$	 235��