   M S B A M L   `   `   `  ????L  HFunctionsDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FunctionsDesignerFunctionsDesigner  5      K*clr-namespace:FunctionsDesigner.ViewModelsFunctionsDesigner.ViewModels  5      k:clr-namespace:FunctionsDesigner.Converters.ValueConverters,FunctionsDesigner.Converters.ValueConverters  5	      C&clr-namespace:FunctionsDesigner.ModelsFunctionsDesigner.Models  5
      W0clr-namespace:FunctionsDesigner.ExtendedControls"FunctionsDesigner.ExtendedControls  5      g cLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc92a3e13d40c573}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5      o<clr-namespace:FunctionsDesigner.ExtendedControls.PointEditor.FunctionsDesigner.ExtendedControls.PointEditor  5      U QLiveChartsCore, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc92a3e13d40c573G4clr-namespace:LiveChartsCore;assembly=LiveChartsCoreLiveChartsCore 5      (    "FunctionsDesigner.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:FunctionsDesigner     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      4vms*clr-namespace:FunctionsDesigner.ViewModels  5      PvalueConverters:clr-namespace:FunctionsDesigner.Converters.ValueConverters  5	      3models&clr-namespace:FunctionsDesigner.Models  5
      GextendedControls0clr-namespace:FunctionsDesigner.ExtendedControls  5      i
liveChartsXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF  5      NpointEditor<clr-namespace:FunctionsDesigner.ExtendedControls.PointEditor  5      IliveChartsCore4clr-namespace:LiveChartsCore;assembly=LiveChartsCore  5        ? Title$  
MainWindow??5      $	??450??5      $	??900??5      $	??450??5      $	??900??5       ? DataContext 5      6   0FunctionsDesigner.ViewModels.MainWindowViewModel  5       ? 	Resources 5      L   FFunctionsDesigner.Converters.ValueConverters.EnumToVisibilityConverter   EnumToVisibilityConverterR   LFunctionsDesigner.Converters.ValueConverters.PointColorToColorBrushConverter # PointColorToColorBrushConverterM   GFunctionsDesigner.Converters.ValueConverters.CountToVisibilityConverter & "CountMoreThanToVisibilityConverter   Type??%j   &	        &	      &	 &       5      6.     5      64     5      $ MoreThan=?5      5       J? InputBindings 5      ?? 5        ?? Gesture$ CTRL+S??5!       ?? Command 5       ?? *SaveProjectCommand+5!      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5#      +  %Microsoft.Xaml.Behaviors.EventTrigger  5$         	EventName$ Closing??6   *  $Microsoft.Xaml.Behaviors.TriggerBase	   Actions.	 5%      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  
   PassEventArgsToCommand$

 True??5&         Command 5%      ?? *WindowClosingRequestedCommand+5&      5'      .??5)      ??  ?? Padding$ 10q?6
   ._?5*      ?  ? ColumnDefinitions 5+      ?? 5,      $
??.55* ?6   ?? 5-      $
??.45* ?6    ? RowDefinitions 5/      ?? 50      $
??Auto ?6   ?? 51      $??* ?6   .+?53      ? $??0??6
   $??1??54      $??0,0,0,10q?55       56      ?? 57      $??* ?6   ?? 58      $??10 ?6   ?? 59      $??* ?6   .+?5<      ?? $??0??6   $??Center=?5=      $??Open project??5>        GeneralBrush#??[ 5?        
WhiteBrush#??[ 5A      #??[ 5B      $??2q?5C      ??5@      ?? *OpenProjectCommand+??5D      ?? 5E       ?? 
TargetType" ??6   .?5F   	   ??  ?? Property ? ?? ?? Value 5G   
   ?? 5H       ?? 
TargetType" ??6   .R?5I      ??  ??Name$ ButtonBorder??6   $
??Auto??5J      $
??Auto??5K      #??z??5L      #??z??5M      #??z??5N      $ 5q?5O       ?? CornerRadius$ 6??5P      ._?5Q      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?5R      .+?5S      F?  F?Name$ AddMark??6   $??24??5T      
??????? ?? Stretch$ Uniform=?5V        OpenFolderIcon#??[ 5W      $??
0, 0, 5, 0q?5X      ?? 5Y       ??Name$ TextContent??6   $??15(?5Z      #??z??5[      $??Center=?5\      $??Medium"?5]      5^      5_       ?? Triggers 5`      P? 5a       P? Property ?@??IsDefaulted P? Value$
 True??5b      . ?5c      ??  ?? 
TargetName$ ButtonBorder??5d       ? ??
 ?   ?5e      5f      P? 5g       ?@J?IsMouseOver$
 True??5h      . ?5i      ??  ?@?Cursor$
 Hand??5j      ?? 5n      $ ButtonBorder??5o      # [ 5p       ? ??5q      P? 5r       ? ??$
 True??5s      . ?5t      ?? $ ButtonBorder??5u       ? ?? ?#CAD6FF5v      ?? 5w      $ ButtonBorder??5x      # [ 5y       ? ???? 5z      $ TextContent??5{        ForegroundWhiteBrush# [ 5|       ? ???? 5}      $ ButtonBorder??5~       ?@J?Opacity$ 1Q?5      5?      5?      5?   
   5?   	   5?      ?? 5?      $??2??6   $??Center=?5?      $??Save project??5?      #??[ 5?      #??[ 5?      #??[ 5?      $??2q?5?      ??5?      ?? *SaveProjectCommand+??5?      ?? 5?      " ??6   .?5?   	   ??  ? ?? 5?   
   ?? 5?      " ??6   .R?5?      ?? $ ButtonBorder??6   $
??Auto??5?      $
??Auto??5?      #??z??5?      #??z??5?      #??z??5?      $ 5q?5?      $ 6??5?      ._?5?      ?? $ 
Horizontal=?6   $??Center=?5?      .+?5?      F? $ AddMark??6   $??20??5?      $ Uniform=?5?      
???????  SaveProjectIcon#??[ 5?      $??
0, 0, 5, 0q?5?      ?? 5?      $ TextContent??6   $??15(?5?      #??z??5?      $??Center=?5?      $??Medium"?5?      5?      5?       5?      P? 5?       ?@??IsDefaulted$
 True??5?      . ?5?      ?? $ ButtonBorder??5?       ? ??
 ?   ?5?      5?      P? 5?       ?@J?IsMouseOver$
 True??5?      . ?5?      ??  ?@?Cursor$
 Hand??5?      ?? 5?      $ ButtonBorder??5?      # [ 5?       ? ??5?      P? 5?       ? ??$
 True??5?      . ?5?      ?? $ ButtonBorder??5?       ? ?? ?#CAD6FF5?      ?? 5?      $ ButtonBorder??5?      # [ 5?       ? ???? 5?      $ TextContent??5?      # [ 5?       ? ???? 5?      $ ButtonBorder??5?       ?@J?Opacity$ 1Q?5?      5?      5?      5?   
   5?   	   5?      5?      ? 5?      $??0??6
   $??2??5?      $??0??5?      .+?5?      5	  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart	  ,
  &LiveChartsCore.SkiaSharpView.WPF.Chart 
  AnimationsSpeed$ 0h?5?      # 
  ChartPointPointerDownCommand$ MoveDown??5?       
  TooltipPosition$ Hidden=?5?       5?       ?? *Chart+  	  XAxes  5?      ?? *XAxes+! 	  YAxes! 5?      ?? *YAxes+" 	  Series" 5?      ?? *Series+5?      5?      ? 5?      $??1??6
   $??1??5?      $??Stretch=?5?       5?      ?? 5?      $??40 ?6   ?? 5?      $??30 ?6   ?? 5?      $??* ?6   ?? 5?      $??60 ?6   .+?5?      ?? $??0??6   $ 
Horizontal=?5?      $??5,0,0,0q?5?      .+?5     ? $??Center=?6    5     ?? 5  	   $
??Auto ?6   ?? 5  	   $??5 ?6   .+?5	     ?? $??0??6   $??
Function: ??5
       
BlackBrush#??[ 5      	 BigFontSize#??[	 5     $??Center=?5     $??SemiBold"?5     ?? 5     $??1??6   $??34??5  	   ??5  	   ?? * SelectedFunction.Series.Stroke+# ?? 	Converter## [ 5     ?? 5     $??15(?5  
   $??5,0,0,0q?5  
   $??10,0,0,0q?5  
   $??SemiBold"?5  
   $??Center=?5  
   $??Center=?5  
   $ ? ToolTip$$ False??5  
    
 ComboBoxStyle#??[
 5  
   $	??160??5  
   $??35??5  
   ??5     ?? *	Functions+% ?? SelectedValue% 5  
   ?? *SelectedFunction+??5      ?? 5!  	   & ?? DataType-   'FunctionsDesigner.ViewModels.FunctionVm"&  6   .R?5"  
   ?  5#     ?? 5$     $
??Auto ?6   ?? 5%     $??* ?6   .+?5'     ?? $??0??6   $
??Left=?5(     ??5)     ?? *Function.Name+A? 5*     $??1??6   $??Right=?5+     $??15??5.     $??15??5/     $??0,0,5,0q?50     $??5??51     ??5,     ?? *Series.Stroke+## [ y?5-     ?? *Function.FunctionType+## [  +   %FunctionsDesigner.Models.FunctionType'   Used( ?? ConverterParameter#( Z' 51     52     53  
   55     ?? 57       RegularButtonTemplate#??[ 6   $??15,0,0,0q?58     $$ Add new function??59     ??5:     ?? *AddFunctionCommand+.??5;     ??   AddIconBlackTemplate#??[ 6   5<     ?? 5>     #??[ 6   $??10,0,0,0q?5?     $$ Remove function??5@     y?5A     ?? *(&SelectedFunction.Function.FunctionType+## [  #( Z' ??5B     ?? *RemoveFunctionCommand+.??5C     ??   RemoveIconTemplate#??[ 6   5D     ?? 5F     #??[ 6   $??10,0,0,0q?5G     $$ Copy to clipboard??5H     $??7q?5J     y?5I     ?? *(&SelectedFunction.Function.FunctionType+## [  #( Z' ??5K     ?? *CopyToClipboardCommand+??5L     F? 5M  	   #??[ 6   $??5??5N       
CopyToIcon#??[ 5O     $ Uniform=?5P     5R     5S     ? 5T     $??1??6   $??Center=?5U     $
??Left=?5V     $??5,0,0,0q?5W     $	??200??5X     y?5Y     ?? *(&SelectedFunction.Function.Points.Count+## [ $( 0?? 5Z     ?? 5[     ?? 5\     $??10 ?6   ?? 5]     .+?5_     ?? $??0??6   $??Center=?5`       SuperBigFontSize#??[ 5a     $
??Bold"?5b     $??X??5c     ?? 5e     $??2??6   $??Center=?5f     #??[ 5g     $
??Bold"?5h     $??Y??5i     5j     ?   9FunctionsDesigner.ExtendedControls.WheelSpeedScrollViewer  5k     $??2??6.   $
??Auto=?5l     $??Disabled=?5m     $
??True??5n     $
??Left=?5o       CustomScrollViewerStyle#??[ 5p     .??5q     ?? $	??240??5r     ??5q     ?? *" SelectedFunction.Function.Points+??5s     ?? 5t  	   &    FunctionsDesigner.Models.PointVm"&  6   .R?5u  
   @   :FunctionsDesigner.ExtendedControls.PointEditor.PointEditor  $??5q?6"   )   
ParameterX) 5v     ?? *X+* ?? Mode$* TwoWay=?+   
ParameterY+ 5w     ?? *Y+$* TwoWay=?,   RemovePointCommand, 5x     ?? *1/DataContext.SelectedFunction.RemovePointCommand+- ?? RelativeSource- ?? *FindAncestor+. ?? AncestorType". ?5y  
   5{     5|     ? 5~     $??3??6    5     ?? 5?     $
??.25* ?6   ?? 5?     $
??.25* ?6   ?? 5?     $
??.35* ?6   ?? 5?     $
??.15* ?6    5?     ?? 5?     ?? 5?     .+?5?       $??0??6   $??0??5?     $??2??5?     $??2??5?     $??Center=?5?     /   DisableDeletion$
/ True??5?     $	??5,0q?5?     ) 5?     ?? * SelectedFunction.NewXParameter+$* TwoWay=?+ 5?     ?? * SelectedFunction.NewYParameter+$* TwoWay=?5?     ?? 5?     $??0??6   $??2??5?     $??2??5?     $??Center=?5?     $??	Add point??5?     $$ Add new point??5?     #??[ 5?     #??[ 5?     #??[ 5?     $??2q?5?     ??5?     ?? *" SelectedFunction.AddPointCommand+??5?     ?? 5?  	   " ??6   .?5?  
   ??  ? ?? 5?     ?? 5?     " ??6   .R?5?     ?? $ ButtonBorder??6   $
??Auto??5?     $
??Auto??5?     #??z??5?     #??z??5?     #??z??5?     $ 5q?5?     $ 6??5?     ._?5?     ?? $ 
Horizontal=?6   $??Center=?5?     .+?5?     F? $ AddMark??6   $??26??5?     $??26??5?     
???????  AddIcon#??[ 5?     $??0, 0, 10, 0q?5?     ?? 5?     $ TextContent??6    $??16(?5?     #??z??5?     $??Center=?5?     $??SemiBold"?5?     5?     5?      5?     P? 5?      ?@??IsDefaulted$
 True??5?     . ?5?     ?? $ ButtonBorder??5?      ? ??
 ?   ?5?     5?     P? 5?      ?@J?IsMouseOver$
 True??5?     . ?5?     ??  ?@?Cursor$
 Hand??5?     ?? 5?     $ ButtonBorder??5?     # [ 5?      ? ??5?     P? 5?      ? ??$
 True??5?     . ?5?     ?? $ ButtonBorder??5?      ? ?? ?#CAD6FF5?     ?? 5?     $ ButtonBorder??5?     # [ 5?      ? ???? 5?     $ TextContent??5?     # [ 5?      ? ???? 5?     $ ButtonBorder??5?      ?@J?Opacity$ 1Q?5?     5?     5?     5?     5?  
   5?     ?? 5?     $??0??6   $??2??5?     $??3??5?     $??40??5?     $??35??5?     $$ Insert from clipboard??5?     #??[ 5?     $	??5,4q?5?     #??[ 5?     $??Center=?5?     ??5?     ?? *InsertFromClipboardCommand+??5?     F? 5?  	   #??[ 6   $??5??5?       InsertFromIcon#??[ 5?     $ UniformToFill=?5?     5?     ??5?     ?? 5?     " ?6   0 ?? Triggers0 5?  	   ?? 5?  
   1 ?? Binding1 6   ?? *IsUnusedFunctionSelected+2 ?? Value2 True5?     .a?5?     ??  ?@J?Opacity$
 0.65Q?5?     ?? 5?      ? {?$ False??5?     5      5  	   5     5     5     5     5     