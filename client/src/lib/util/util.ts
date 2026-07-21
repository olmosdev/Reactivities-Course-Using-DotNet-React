import { type DateArg, format } from "date-fns";

export function formatDate(date: DateArg<Date>): string {
  return format(date, "dd MMM yyyy h:mm aa");
}
