import { Badge } from "@/components/ui/badge";
import type { PaymentId, PaymentMethod } from "@/types/entities";
import { Label } from "@radix-ui/react-label";

const PaymentOption = ({
  option,
  selected,
  onSelect,
}: {
  option: PaymentMethod;
  selected: boolean;
  onSelect: (id: PaymentId) => void;
}) => {
  const { id, title, icon, badges, disabled } = option;
  return (
    <div
      role="button"
      aria-pressed={selected}
      onClick={() => !disabled && onSelect(id)}
      className={`
    flex items-center gap-4 rounded-xl border border-white/10 p-5 transition
    ${disabled ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}
    ${selected ? "bg-green-600" : "bg-white/10 hover:bg-white/20"}
  `}
    >
      {icon}
      <div className="flex flex-col">
        <Label className="text-xl font-medium">{title}</Label>
        {badges?.map((badge, index) => (
          <Badge key={index} className={`mt-1 ${badge.color}`}>
            {badge.text}
          </Badge>
        ))}
      </div>
    </div>
  );
};

export default PaymentOption;
