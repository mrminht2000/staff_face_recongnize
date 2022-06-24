import { FileExport } from "../models/file-export";

export const DefaultPaging = {
    pageSizeOptions: [50, 100, 150, 200, 500],
    defaultPageSize: 50
};

export const ExcelFile : FileExport = {
    fileExtension: 'xlsx',
    fileType: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8',
    extensionName: 'Excel'
}

export const CSVFile : FileExport = {
    fileExtension: 'csv',
    fileType: 'text/csv;application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8',
    extensionName: 'CSV'
}

export const MaxExportValue : number = 1000;

export const authToken = "authToken";

export enum Role {
    User, Admin, Manager
}