   M S B A M L   `   `   `  ����  vkManageFinal"    vkManageFinal.SearchUserById   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$)  #Пошук користувачів��5   	   $��124.622��6$   $��261.58��65    � WindowStartupLocation$ CenterScreen=�6E    � 
ResizeMode$ NoResize=�6j    � Icon$" Images/vkontakte-128x128.png��6�    � Triggers 5      :� 5   
    :� RoutedEvent$ Loaded��6   .[�5      �� .��5      �� .��5	      W�  �� TargetProperty$ Opacity�5
       W� From$ 0X�5      	 W� To$ 1X�5      	 g� Duration$	 0:0:1D�5      
 g� DecelerationRatio$	
 0.8Q�5      5      5      5      .��5      �� -   5         ��Name$ GetUsersPanel��5   (   ���	#FF787878$��96��6?   $	��260��6K   .+�5   
   �� $��Center=�6   $:��4Введіть id/Screen Name користувача��6-   $��	26,0,10,0q�6Z   $	��224��6m   �� 5   
   -   5         ��Name$ UserId��5      $��22��6"   $
��45,5q�6.    �� TextWrapping$
 Wrap=�6<   �� 5   
   -   5         ��Name$ ExecuteSearch��5      $
��45,0q�6(   $��22��66   $��Знайти��6B   6S   5      5      