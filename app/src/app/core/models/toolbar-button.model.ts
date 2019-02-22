export class ToolbarButtonModel {
  public display: string;
  public href: string;
  public disabled: boolean;
  public icon: string;
  public submit: boolean;
  public hidden: boolean;
  public useGridQuery: boolean;
  public action: () => {};
  public actionQuery: (query: string) => {};
}
