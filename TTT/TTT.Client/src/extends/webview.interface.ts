export interface WebviewInterface {
    showModule(moduleName: string): Promise<void>;
    hideModule(moduleName: string): Promise<void>;
  }